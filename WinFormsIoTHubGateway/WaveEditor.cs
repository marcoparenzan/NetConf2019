using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WinFormsCoreLib.Controls;
using WinFormsCoreLib.ControlsExtensions;
using WinFormsCoreLib.Forms;

namespace WinFormIoTHubGateway
{
    public static partial class WaveEditor
    {
        public static Form CreateNew()
        {
            var form = FormBase.New();

            var drawingBox = form
                .New<DrawingBox>();
            drawingBox
                .Location(16)
                .Size(-16, -60)
                .OnMouseMove((s, e) => {
                })
                .AnchorAll()
            ;
            form.OnKeyDown((s, e) =>
            {
                if (e.KeyCode == Keys.C && e.Control)
                {
                    Copy(drawingBox);
                }
                else if (e.KeyCode == Keys.X && e.Control)
                {
                    Copy(drawingBox);
                    Clear(drawingBox);
                }
                else if (e.KeyCode == Keys.V && e.Control)
                {
                    Paste(drawingBox, e.Alt);
                }
            })
            ;
            form.KeyPreview = true;

            return form;
        }

        private static void Clear(DrawingBox drawingBox)
        {
            drawingBox.Bitmap.Clear();
        }

        private static void Copy(DrawingBox drawingBox)
        {
            List<float?> scans = CopyImpl(drawingBox);
            var text = JsonConvert.SerializeObject(scans);
            Clipboard.SetText(text, TextDataFormat.Text);
        }

        private static List<float?> CopyImpl(DrawingBox drawingBox)
        {
            var bitmap = drawingBox.Bitmap;
            var scans = new List<float?>();
            for (var x = 0; x < bitmap.Current.Width; x++)
            {
                var cs = new List<float>();
                for (var y = bitmap.Current.Height - 1; y >= 0; y--)
                {
                    var color = bitmap.Current.GetPixel(x, y);
                    if (bitmap.IsTransparent(color)) continue; // if transparent...no way
                    var yy = (float)(bitmap.Current.Height - y) / (float)bitmap.Current.Height;
                    cs.Add(yy);
                }
                if (cs.Any())
                    scans.Add(cs.Average());
                else
                    scans.Add(null);
            }

            return scans;
        }

        private static void Paste(DrawingBox drawingBox, bool withClear = false)
        {
            if (!Clipboard.ContainsText(TextDataFormat.Text)) return;
            var jsonTentative = Clipboard.GetText(TextDataFormat.Text);
            try
            {
                var scans = JsonConvert.DeserializeObject<float?[]>(jsonTentative);
                if (withClear) Clear(drawingBox);
                PasteImpl(drawingBox, scans);
            }
            catch
            {
                // nothing to do
            }
        }

        private static void PasteImpl(DrawingBox drawingBox, float?[] scans)
        {
            var bitmap = drawingBox.Bitmap;
            bitmap.Clear();

            var dx = (float)scans.Length / (float)bitmap.Current.Width;
            var sx = 0.0f;

            bool lastWasNull = true;
            for (var x = 0; x < bitmap.Current.Width; x++)
            {
                var scan = scans[(int)sx];
                if (scan == null)
                {
                    sx += dx;
                    lastWasNull = true;
                    continue;
                }

                var y = (int)((1.0f - scan) * (float)bitmap.Current.Height);
                bitmap.Current.SetPixel(x, y, System.Drawing.Color.Red);
                if (lastWasNull)
                {
                    bitmap.MoveTo(x, y);
                    lastWasNull = false;
                }
                else
                {
                    bitmap.DrawTo(x, y);
                }
            }
            drawingBox.Invalidate();
        }
    }
}
