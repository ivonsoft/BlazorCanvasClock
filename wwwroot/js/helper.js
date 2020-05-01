window.getWindowSize = () => {
    return {
        height: window.innerHeight,
        width: window.innerWidth
    };
};
((window) => {
    let canvasContextCache = {};

    let getContext = (canvas) => {
        console.log("get context:", canvas.id);
        if (!canvasContextCache[canvas.id]) {
            /*  
                // I thought this assignemnts should refer exactly to context, but eventually does not work when switching between blazor pages where component is displayed!!!!
                canvasContextCache[canvas.id] = canvas.getContext("2d"); 
            */
           /* that is why I've choosen conventional method to grab canvas objects, it works when switching between blazor pages */
            canvasContextCache[canvas.id] = document.getElementById(canvas.id).getContext("2d");
        }
        return canvasContextCache[canvas.id];
    };
    let degToRad = (degree) => {
        var factor = Math.PI / 180;
        return degree * factor;
    };
    window._CanvasInterop = {
        drawLine: (canvas, sX, sY, eX, eY) => {
            let context = getContext(canvas);

            context.lineJoin = 'round';
            context.lineWidth = 5;
            context.beginPath();
            context.moveTo(eX, eY);
            context.lineTo(sX, sY);
            context.closePath();
            context.stroke();
        },
        drawText: (canvas, text, eX, eY) => {
            let context = getContext(canvas);
            var gradient = context.createLinearGradient(Math.round(canvas.width * 0.3, 0), 0, Math.round(canvas.width * 0.6, 0), 0);
            gradient.addColorStop("0", "magenta");
            gradient.addColorStop("0.5", "blue");
            gradient.addColorStop("1.0", "red");
            // Fill with gradient
            context.fillStyle = gradient;
            context.strokeText(text, canvas.width / 2 - context.measureText(text).width / 2, canvas.height / 2);
        },
        clearCanvas: (canvas) => {
            let context = getContext(canvas);
            var w = canvas.width;
            var str = context['strokeStyle'];
            var font = context['font'];
            canvas.width = 1;
            context.clearRect(0, 0, canvas.width, canvas.height);
            canvas.width = w;
            context['strokeStyle'] = str;
            context['font'] = font;
        },
        createBackground: (canvas, halfSize) => {
            let context = getContext(canvas);
            var gradient = context.createRadialGradient(halfSize, halfSize, 1, halfSize, halfSize, halfSize * 1.2);
            gradient.addColorStop(0, '#09303a');
            gradient.addColorStop(1, 'black');

            //background
            context.shadowBlur = 0;
            context.fillStyle = gradient;
            context.strokeStyle = 'black';
            context.lineWidth = '1';
            context.beginPath();
            context.arc(halfSize, halfSize, halfSize * 0.996, 0, degToRad(360));
            context.stroke();
            context.fill();
        },
        drawHours: (canvas, halfSize, newHours) => {
            let context = getContext(canvas);
            //hours
            var lineWidth = halfSize * 0.12;
            var shadowBlur = halfSize * 25;
            context.shadowBlur = shadowBlur;
            // context.strokeStyle = '#28d1fa';
            context.lineWidth = lineWidth;
            context.beginPath();
            context.arc(halfSize, halfSize, 0.82 * halfSize, degToRad(270), degToRad((newHours * 30) - 90));
            context.stroke();
        },
        drawMinutes: (canvas, halfSize, newMinutes) => {
            let context = getContext(canvas);
            //hours
            var lineWidth = halfSize * 0.12;
            var shadowBlur = halfSize * 25;
            context.shadowBlur = shadowBlur;
            // context.strokeStyle = '#28d1fa';
            context.lineWidth = lineWidth;
            context.beginPath();
            context.arc(halfSize, halfSize, 0.68 * halfSize, degToRad(270), degToRad((newMinutes * 6) - 90));
            context.stroke();
        },
        drawSeconds: (canvas, halfSize, newSeconds) => {
            let context = getContext(canvas);
            //hours
            var lineWidth = halfSize * 0.12;
            var shadowBlur = halfSize * 25;
            context.shadowBlur = shadowBlur;
            // context.strokeStyle = '#28d1fa';
            context.lineWidth = lineWidth;
            context.beginPath();
            context.arc(halfSize, halfSize, 0.54 * halfSize, degToRad(270), degToRad((newSeconds * 6) - 90));
            context.stroke();
        },
        drawDateTime: (canvas, halfSize, today, time) => {
            let context = getContext(canvas);
            //hours
            //  context.font = timeFont;
            var gradient = context.createRadialGradient(halfSize, halfSize, 1, halfSize, halfSize, halfSize * 1.2);
            gradient.addColorStop(0, '#09303a');
            gradient.addColorStop(1, 'black');
            var str = context['strokeStyle'];
            var font = context['font'];
             //background
            context.shadowBlur = 0;
            context.fillStyle = gradient;
            context.strokeStyle = 'black';
            context.lineWidth = '1';
            context.beginPath();
            context.arc(halfSize, halfSize, halfSize * 0.396, 0, degToRad(360));
            context.stroke();
            context.fill();
          //  context.fillStyle = '#28d1fa';
            context['strokeStyle'] = str;
            context['fillStyle'] = str;
            context['font'] = font;
            
            context.save();
            context.scale(2,1.8);
            //context.font = dateFont;
            context.fillText(time, halfSize * 0.366, halfSize*0.649 );
            context.restore(); 
            context.fillText(today, halfSize * 0.756, halfSize);
          
        },
        setContextPropertyValue: (canvas, propertyName, propertyValue) => {
            let context = getContext(canvas);
            context[propertyName] = propertyValue;
        }
    };
})(window);