using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace Bot_Application1.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }


        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            // calculate something for us to return
            int length = (activity.Text ?? string.Empty).Length;

            // return our reply to the user
            await context.PostAsync($"You sent {activity.Text} which was {length} characters");

            context.Wait(MessageReceivedAsync);
        }
    }

    [Serializable]
    public class SimpleDialog : IDialog
    {
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(ActivityReceivedAsync);
        }

        public async Task ActivityReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            String text = activity.Text;
            if (text.Contains("-"))
            {
                String[] stringTokens = text.Split('-');
                if (stringTokens.Length == 2)
                {
                    int teamScore = 0;
                    int oppScore = 0;

                    if (int.TryParse(stringTokens[0], out teamScore))
                    { }
                    if (int.TryParse(stringTokens[1], out oppScore))
                    { }

                    if (teamScore > oppScore)
                        await context.PostAsync("Yeah, a Win!!");
                    else if (teamScore < oppScore)
                        await context.PostAsync("Sorry, we lost :(");
                    else if (teamScore == oppScore)
                        await context.PostAsync("We'll take a tie");

                    //if (int.TryParse(stringTokens[0], out intScore))
                    //{
                    //    if (intScore >= 6)
                    //        await context.PostAsync("Yaaay");
                    //    else if (intScore > 4)
                    //        await context.PostAsync("We are making good progress");
                    //    else if (intScore > 2)
                    //        await context.PostAsync("We are making progress");
                    //    else if (intScore >= 0)
                    //        await context.PostAsync("We are playing OK");
                    //}
                }
            }

            context.Wait(ActivityReceivedAsync);
        }
    }
}