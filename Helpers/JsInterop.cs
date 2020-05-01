using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using System;

namespace BlazorClock.Canvas
{
    public struct WindowSize
    {
        public long Height { get; set; }
        public long Width { get; set; }

    }

    public class Canvas2DContext
    {
        private readonly IJSRuntime jsRuntime;
      //  private readonly string canvasID;
        private readonly ElementReference canvasRef;

        public Canvas2DContext(IJSRuntime jsRuntime, ElementReference canvasRef)
        {
            this.jsRuntime = jsRuntime;
            this.canvasRef = canvasRef;
            //Console.WriteLine("init canvas2D");
        }

        public async Task DrawLine(long startX, long startY, long endX, long endY)
        {
            _ = await jsRuntime.InvokeAsync<object>("_CanvasInterop.drawLine", canvasRef, startX, startY, endX, endY);
        }
        public async Task DrawHours(long halfsize, double newHours)
        {
            _ = await jsRuntime.InvokeAsync<object>("_CanvasInterop.drawHours", canvasRef, halfsize, newHours);
        }
        public async Task DrawMinutes(long halfsize, double newMins)
        {
            _ = await jsRuntime.InvokeAsync<object>("_CanvasInterop.drawMinutes", canvasRef, halfsize, newMins);
        }
        public async Task DrawSeconds(long halfsize, double newSecs)
        {
            _ = await jsRuntime.InvokeAsync<object>("_CanvasInterop.drawSeconds", canvasRef, halfsize, newSecs);
        }
        public async Task DrawBackground(long halfsize)
        {
            _ = await jsRuntime.InvokeAsync<object>("_CanvasInterop.createBackground", canvasRef, halfsize);
        }
        public async Task DrawText(string text, long eX, long eY)
        {

            _ = await jsRuntime.InvokeAsync<object>("_CanvasInterop.drawText", canvasRef, text, eX, eY);
        }
        public async Task DrawDateTime(long halfsize, string  Data, string czas)
        {

            _ = await jsRuntime.InvokeAsync<object>("_CanvasInterop.drawDateTime", canvasRef, halfsize, Data, czas);
        }
        public async Task ClearCanvas()
        {
            _ = await jsRuntime.InvokeAsync<object>("_CanvasInterop.clearCanvas", canvasRef);
        }
        public async Task SetStrokeStyleAsync(string strokeStyle)
        {
            await jsRuntime.InvokeAsync<object>("_CanvasInterop.setContextPropertyValue", canvasRef, "strokeStyle", strokeStyle);
        }
        public async Task SetfontAsync(string font)
        {
            await jsRuntime.InvokeAsync<object>("_CanvasInterop.setContextPropertyValue", canvasRef, "font", font);
        }
    }

}

