using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using ProductsBot.Models;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
//using Microsoft.Bot.Builder.Luis;​
//using Microsoft.Bot.Builder.Luis.Models;

namespace ProductsBot.Dialogs
{
    [Serializable]
    //[LuisModel("92b51188-38f9-457a-b33e-970ac05d08db", "facb3be9b9d84fa8bf9db76cb88e54fc")]

    public class RootDialog : IDialog<Object>//LuisDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(ShowCategoriesAsync);

            return Task.CompletedTask;
        }

        private async Task ShowCategoriesAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;
            var reply = activity.CreateReply("Choose category");
            reply.Type = ActivityTypes.Message;
            reply.TextFormat = TextFormatTypes.Plain;

            var categories = await GetAllCategoriesAsync("http://localhost:60963/api/products/allcategories");

            var categoriesActions = new List<CardAction>();
            foreach (var category in categories)
            {
                categoriesActions.Add(
                    new CardAction() { Title = category.CategoryName, Type = ActionTypes.PostBack, Value = category.Id });
            }

            reply.SuggestedActions = new SuggestedActions()
            {
                Actions = categoriesActions
            };

            await context.PostAsync("Hi! I'm a shopping bot.");
            await context.PostAsync(reply);

            context.Wait(CategoryReceivedSendCardsAsync);
        }

        private async Task<IEnumerable<Category>> GetAllCategoriesAsync(string uri)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(uri);
            var content = await response.Content.ReadAsAsync<IEnumerable<Category>>();
            return content;

        }

        private Attachment CreateAttachmentForProduct(Product product)
        {
            List<CardAction> cardButtons = new List<CardAction>();

            CardAction plButton = new CardAction()
            {
                Value = product.Id,
                Type = ActionTypes.PostBack,
                Title = "Add to cart"
            };

            cardButtons.Add(plButton);

            HeroCard plCard = new HeroCard()
            {
                Title = $"{product.Name}   $ {product.Price}",
                Subtitle = product.Info,
                Buttons = cardButtons
            };

            return plCard.ToAttachment();

        }

        private async Task CategoryReceivedSendCardsAsync(IDialogContext context, IAwaitable<object> result)
        {

            var message = await result as Activity;

            var categoryId = Guid.Parse(message.Text);

            Activity replyToConversation = message.CreateReply("Products from selected category:");
            replyToConversation.Attachments = new List<Attachment>();

            var productsByCategory =
                await GetProductsByCategoryAsync("http://localhost:60963/api/products/bycategory/" + categoryId.ToString());

            foreach (var product in productsByCategory)
            {
                replyToConversation.Attachments.Add(CreateAttachmentForProduct(product));
            }
          

            var connector = new ConnectorClient(new Uri(message.ServiceUrl));
            var reply = await connector.Conversations.SendToConversationAsync(replyToConversation);
            context.Wait(AddProductToCartAsync);
        }

        private async Task ShowMenuAsync(IDialogContext context, IAwaitable<object> result)
        {
            var message = await result as Activity;
            if (message.Text == "y")
                context.Wait(BuyProductsAsync);
            if (message.Text == "n")
                await context.PostAsync("ok, bought");
        }

        private async Task AddProductToCartAsync(IDialogContext context, IAwaitable<object> result)
        {
            var message = await result as Activity;
           
            var productId = Guid.Parse(message.Text);
            var userId = message.From.Id;

            string uri = "http://localhost:60963/api/cart/addproduct/" + userId + "/" + productId.ToString();
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.PostAsync(uri, new StringContent("text"));
            var content = await response.Content.ReadAsAsync<ShoppingCart>();
            await context.PostAsync("Product added to cart");
            var reply = message.CreateReply("Do you want to buy all products from cart7");

            var buyActions = new List<CardAction>()
            {
                 new CardAction() { Title = "Buy", Type = ActionTypes.PostBack, Value = "buy" },
                 new CardAction() { Title = "Continue shopping", Type = ActionTypes.PostBack, Value = "continue" }

            };
            reply.SuggestedActions = new SuggestedActions()
            {
                Actions = buyActions
            };
            
            await context.PostAsync(reply);

            context.Wait(RedirectAsync);

        }

        private async Task RedirectAsync(IDialogContext context, IAwaitable<object> result)
        {
            var message = await result as Activity;
            if (message.Text.Contains("buy"))
                context.Wait(BuyProductsAsync);

            if (message.Text.Contains("continue"))
                context.Wait(ShowCategoriesAsync);

        }

        private async Task BuyProductsAsync(IDialogContext context, IAwaitable<object> result)
        {
            await context.PostAsync("In buy");

            var message = await result as Activity;
            var userId = message.From.Id;
            HttpClient httpClient = new HttpClient();
            string uri = "http://localhost:60963/api/cart/buyproducts/" + userId;
            HttpResponseMessage response = await httpClient.PostAsync(uri, new StringContent("text"));
            var content = await response.Content.ReadAsAsync<bool>();
            await context.PostAsync("bought");
            context.Wait(ShowCategoriesAsync);
            
        }

        private async Task<IEnumerable<Product>> GetProductsByCategoryAsync(string uri)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(uri);
            var content = await response.Content.ReadAsAsync<IEnumerable<Product>>();
            return content;

        }


    }

}