namespace CherwellTriangles.Model
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;

    using CherwellTriangles.Resolvers;

    public interface ITriangleViewModel
    {
        string Ref { get; }
        
        string Image { get; }

        string CoordsString { get; }

        void Initialise(PointF rightAngle, PointF topLeft);

        void Initialise(char letter, int number);
    }

    public class TriangleViewModel : ITriangleViewModel
    {
        private readonly ITriangleResolver _resolver;

        public TriangleViewModel(ITriangleResolver resolver)
        {
            this._resolver = resolver;
        }

        public string Ref { get; private set; }

        public string Image { get; private set; }

        public string CoordsString { get; private set; }

        private PointF BottomRight { get; set; }

        private PointF RightAngle { get; set; }

        private PointF TopLeft { get; set; }
        
        public void Initialise(PointF rightAngle, PointF topLeft)
        {
            this.RightAngle = rightAngle;
            this.TopLeft = topLeft;
            this.BottomRight = new PointF(topLeft.X + 10, topLeft.Y + 10);

            this.Ref = this._resolver.Resolve(this.RightAngle, this.TopLeft, this.BottomRight);

            this.Image = this.ToImage();
            this.CoordsString = this.CoordsToString();
        }

        public void Initialise(char letter, int number)
        {
            this.Ref = $"({letter}, {number})";

            var coords = this._resolver.Resolve(letter, number);
            this.RightAngle = coords[0];
            this.TopLeft = coords[1];
            this.BottomRight = coords[2];

            this.Image = this.ToImage();
            this.CoordsString = this.CoordsToString();
        }

        public string ToImage()
        {
            var bitmap = new Bitmap(60, 60);

            using (var graphics = Graphics.FromImage(bitmap))
            {
                var pen = new Pen(Color.Black);
                var brush = new SolidBrush(Color.MidnightBlue);

                graphics.DrawLine(pen, 0, 0, 59, 0);
                graphics.DrawLine(pen, 59, 0, 59, 59);
                graphics.DrawLine(pen, 59, 59, 0, 59);
                graphics.DrawLine(pen, 0, 59, 0, 0);

                graphics.FillPolygon(
                    brush,
                    new List<PointF> { this.TopLeft, this.BottomRight, this.RightAngle }.ToArray());

                brush.Dispose();
                graphics.Dispose();
            }

            using (var saveStream = new MemoryStream())
            {
                bitmap.Save(saveStream, GetEncoder(ImageFormat.Png), EncoderParameters());
                return $"data:image/png;base64,{Convert.ToBase64String(saveStream.ToArray())}";
            }
        }

        public string CoordsToString()
        {
            return $"Right Angle: ({this.RightAngle.X},{this.RightAngle.Y}){Environment.NewLine}"
                + $"Top Left: ({this.TopLeft.X},{this.TopLeft.Y}){Environment.NewLine}"
                + $"Bottom Right: ({this.BottomRight.X},{this.BottomRight.Y}){Environment.NewLine}";
        }

        private static EncoderParameters EncoderParameters()
        {
            var encoder = Encoder.Quality;
            var encoderParameters = new EncoderParameters(1);

            var encoderParameter = new EncoderParameter(encoder, 50L);
            encoderParameters.Param[0] = encoderParameter;
            return encoderParameters;
        }

        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            var codecs = ImageCodecInfo.GetImageDecoders();
            return codecs.FirstOrDefault(codec => codec.FormatID == format.Guid);
        }
    }
}