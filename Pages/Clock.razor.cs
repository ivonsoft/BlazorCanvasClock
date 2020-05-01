using System;
using Microsoft.AspNetCore.Components;
using BlazorClock.Canvas;
using Microsoft.JSInterop;
using System.Threading;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components.Web;
using System.Threading.Tasks;

namespace BlazorClockCanvas
{
    public partial class ClockModel : ComponentBase, IDisposable
    {

        protected WindowSize windowSize;
        private bool minuteRefreshed = false;
        private string prevColour;
        protected ElementReference canvas;
        protected string canvasID;
        private Task ClockTask;
        private Boolean firstRun = true;
        private string canvasFont = "16px Digital-7";
        protected Canvas2DContext ctx;
        private string actualColor = "#F4908E";
        protected IEnumerable<string> colours = new[] { "#F4908E", "#F2F097", "#88B0DC", "#F7B5D1", "#53C4AF", "#FDE38C" };
        [Inject] protected IJSRuntime JSRuntime { get; set; }
        protected Action<MouseEventArgs> SetStrokeColour(string colour)
        {
            return async _ =>
            {
                actualColor = colour;
                //Console.WriteLine($"ustawiono kolor: {colour}");
                await ctx.SetStrokeStyleAsync(colour);
                await ctx.SetfontAsync(canvasFont);

            };
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                windowSize.Height = 400;
                windowSize.Width = 400;
                Console.WriteLine($" canavsID: {canvasID}, {windowSize.Height}, {windowSize.Width}");
                ctx = new Canvas2DContext(JSRuntime, canvas);

                ClockTask = RunClock();
                await this.ctx.DrawBackground(windowSize.Height / 2);
                await ctx.SetStrokeStyleAsync(actualColor);
                await ctx.SetfontAsync(canvasFont);
            }
            
        }
        protected override void OnInitialized()
        {
            canvasID = Guid.NewGuid().ToString("d").Substring(1, 6);
        }
        private async Task DrawClockElectronicText(long halfSize, string time,  int hr, int min, int sec )
        {
            string Thours = (hr < 10 ? "0" : "") + hr.ToString();
            string Tminutes = (min < 10 ? "0" : "") + min.ToString();
            string Tseconds = (sec < 10 ? "0" : "") + sec.ToString();
            await this.ctx.DrawDateTime(halfSize,time,$"{Thours}:{Tminutes}:{Tseconds}");
        }
        private double degToRad(double degree)
        {
            var factor = Math.PI / 180;
            return degree * factor;
        }
        // private System.Threading.Timer timer = null;
        private async Task RunClock()
        {
            await Task.Delay(40);
            while (true)
            {
                await updateClock();
                await Task.Delay(100);
            }
        }
            
        private async Task updateClock()
        {
            var size = windowSize.Height;
            var halfSize = size / 2;
            var timeFontSize = halfSize * 0.06;
            var dateFontSize = halfSize * 0.12;
            var timeFont = $"{timeFontSize} px helvetica";
            var dateFont = $"{dateFontSize} px helvetica";

            DateTime now = DateTime.Now;
            var today = now.Date;
            var time = now.ToLongDateString();
            var hours = now.Hour;
            var minutes = now.Minute;
            var seconds = now.Second;
            var milliseconds = now.Millisecond;
            float newSeconds = seconds*1.0f + milliseconds*1.0f / 1000;
            if (seconds>0){
                minuteRefreshed = true;
            }
            float newMinutes = minutes + seconds*1.0f / 60 + milliseconds*1.0f / 60000;
            float newHours = hours + minutes*1.0f / 60 + seconds*1.0f / 3600 + milliseconds*1.0f / 3600000;
            //var timeArray = time(' ');
            //time = timeArray[0] + ' ' + timeArray[1];
            if ( firstRun || (seconds == 0 && minuteRefreshed) || actualColor != prevColour)
            {
                minuteRefreshed = false;
                prevColour = actualColor;
                await this.ctx.DrawBackground(halfSize);
                await ctx.SetStrokeStyleAsync(actualColor);
                await this.ctx.DrawMinutes(halfSize, newMinutes);
                await this.ctx.DrawHours(halfSize, newHours);
                firstRun = false;
            }
            await this.ctx.DrawSeconds(halfSize, newSeconds);
           await this.DrawClockElectronicText(halfSize,time, hours,minutes,seconds);
        }


        void IDisposable.Dispose()
        {
            ClockTask = null;
        }
    }
}