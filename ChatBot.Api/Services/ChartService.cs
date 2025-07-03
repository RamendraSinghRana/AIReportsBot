using SkiaSharp;

public class ChartService : IChartService
{
    public byte[] GenerateChart(string chartType, List<DataModel> data)
    {
        int width = 800, height = 400;
        // Create a new SKBitmap to draw the chart
        using (var bitmap = new SKBitmap(width, height))
        using (var canvas = new SKCanvas(bitmap))
        {
            // Clear the canvas with a white background
            canvas.Clear(SKColors.White);

            // Set up paint for drawing
            var paint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = SKColors.Blue
            };
            var font = new SKFont
            {
                Size = 20
            };

            // Draw the chart based on the type
            if (chartType.ToLower() == "bar")
            {
                float barWidth = width / (data.Count * 2);
                float maxVal = data.Max(d => float.Parse(d.Value));

                for (int i = 0; i < data.Count; i++)
                {
                    float value = float.Parse(data[i].Value);
                    float barHeight = (float)(value / maxVal * height * 0.8); // 80% of height for the bar
                    float x = i * barWidth * 2 + barWidth / 2; // Calculate x position for each bar
                    canvas.DrawText(data[i].Category, x, height - 10, font, paint); // Draw category label below the bar
                    float y = height - barHeight - 40; // 40px padding from the bottom
                    canvas.DrawRect(x, y, barWidth, barHeight, paint);
                    // Removed canvasDrawText as it's not a valid SkiaSharp method and already drawing text above
                }
            }
            else if (chartType == "line")
            {
                DrawLineChart(canvas, data, paint, width, height);
            }
            // Add more chart types as needed

            // Encode the bitmap to PNG format
            using (var image = SKImage.FromBitmap(bitmap))
            using (var dataStream = image.Encode(SKEncodedImageFormat.Png, 100))
            {
                return dataStream.ToArray();
            }
        }
    }

    private void DrawLineChart(SKCanvas canvas, List<DataModel> data, SKPaint paint, int width, int height)
    {
        if (data == null || data.Count < 2)
            return;

        float maxVal = data.Max(d => float.Parse(d.Value));
        float minVal = data.Min(d => float.Parse(d.Value));
        float range = maxVal - minVal;
        float stepX = width / (float)(data.Count - 1);

        using (var linePaint = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            Color = SKColors.Red,
            StrokeWidth = 3
        })
        {
            for (int i = 0; i < data.Count - 1; i++)
            {
                float x1 = i * stepX;
                float y1 = height - 40 - ((float.Parse(data[i].Value) - minVal) / (range == 0 ? 1 : range) * (height * 0.8f));
                float x2 = (i + 1) * stepX;
                float y2 = height - 40 - ((float.Parse(data[i + 1].Value) - minVal) / (range == 0 ? 1 : range) * (height * 0.8f));
                canvas.DrawLine(x1, y1, x2, y2, linePaint);
            }
        }
    }
}