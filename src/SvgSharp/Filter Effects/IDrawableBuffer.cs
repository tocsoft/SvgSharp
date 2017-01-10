using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Drawing;
//using System.Drawing.Imaging;
//using System.Drawing.Drawing2D;

namespace Svg.FilterEffects
{
    public interface IDrawable
    {
    }

    public class DrawableBuffer : IDictionary<string, IDrawable>
    {
        private const string BufferKey = "__!!BUFFER";

        private Dictionary<string, IDrawable> _images;
        private RectangleF _bounds;
        private ISvgRenderer _renderer;
        private Action<ISvgRenderer> _renderMethod;
        private float _inflate;

        public Matrix Transform { get; set; }

        public IDrawable Buffer
        {
            get { return _images[BufferKey]; }
        }
        public int Count
        {
            get { return _images.Count; }
        }
        public IDrawable this[string key]
        {
            get
            {
                return ProcessResult(key, _images[ProcessKey(key)]);
            }
            set
            {
                _images[ProcessKey(key)] = value;
                if (key != null) _images[BufferKey] = value;
            }
        }

        public DrawableBuffer(RectangleF bounds, float inflate, ISvgRenderer renderer, Action<ISvgRenderer> renderMethod)
        {
            _bounds = bounds;
            _inflate = inflate;
            _renderer = renderer;
            _renderMethod = renderMethod;
            _images = new Dictionary<string, IDrawable>();
            _images[SvgFilterPrimitive.BackgroundAlpha] = null;
            _images[SvgFilterPrimitive.BackgroundImage] = null;
            _images[SvgFilterPrimitive.FillPaint] = null;
            _images[SvgFilterPrimitive.SourceAlpha] = null;
            _images[SvgFilterPrimitive.SourceGraphic] = null;
            _images[SvgFilterPrimitive.StrokePaint] = null;
        }

        public void Add(string key, IDrawable value)
        {
            _images.Add(ProcessKey(key), value);
        }
        public bool ContainsKey(string key)
        {
            return _images.ContainsKey(ProcessKey(key));
        }
        public void Clear()
        {
            _images.Clear();
        }
        public IEnumerator<KeyValuePair<string, IDrawable>> GetEnumerator()
        {
            return _images.GetEnumerator();
        }
        public bool Remove(string key)
        {
            switch (key)
            {
                case SvgFilterPrimitive.BackgroundAlpha:
                case SvgFilterPrimitive.BackgroundImage:
                case SvgFilterPrimitive.FillPaint:
                case SvgFilterPrimitive.SourceAlpha:
                case SvgFilterPrimitive.SourceGraphic:
                case SvgFilterPrimitive.StrokePaint:
                    return false;
                default:
                    return _images.Remove(ProcessKey(key));
            }
        }
        public bool TryGetValue(string key, out IDrawable value)
        {
            if (_images.TryGetValue(ProcessKey(key), out value))
            {
                value = ProcessResult(key, value);
                return true;
            }
            else
            {
                return false;
            }
        }

        private IDrawable ProcessResult(string key, IDrawable curr)
        {
            if (curr == null)
            {
                switch (key)
                {
                    case SvgFilterPrimitive.BackgroundAlpha:
                    case SvgFilterPrimitive.BackgroundImage:
                    case SvgFilterPrimitive.FillPaint:
                    case SvgFilterPrimitive.StrokePaint:
                        // Do nothing
                        return null;
                    case SvgFilterPrimitive.SourceAlpha:
                        _images[key] = CreateSourceAlpha();
                        return _images[key];
                    case SvgFilterPrimitive.SourceGraphic:
                        _images[key] = CreateSourceGraphic();
                        return _images[key];
                }
            }
            return curr;
        }
        private string ProcessKey(string key)
        {
            if (string.IsNullOrEmpty(key)) return _images.ContainsKey(BufferKey) ? BufferKey : SvgFilterPrimitive.SourceGraphic;
            return key;
        }



        private IDrawable CreateSourceGraphic()
        {
            throw new NotImplementedException();

            //var graphic = new Bitmap((int)(_bounds.Width + 2 * _inflate * _bounds.Width + _bounds.X),
            //                         (int)(_bounds.Height + 2 * _inflate * _bounds.Height + _bounds.Y));
            //using (var renderer = SvgRenderer.FromImage(graphic))
            //{
            //    renderer.SetBoundable(_renderer.GetBoundable());
            //    var transform = new Matrix();
            //    transform.Translate(_bounds.Width * _inflate, _bounds.Height * _inflate);
            //    renderer.Transform = transform;
            //    //renderer.Transform = _renderer.Transform;
            //    //renderer.Clip = _renderer.Clip;
            //    _renderMethod.Invoke(renderer);
            //}
            //return graphic;
        }

        private IDrawable CreateSourceAlpha()
        {
            throw new NotImplementedException();

            //IDrawable source = this[SvgFilterPrimitive.SourceGraphic];

            //float[][] colorMatrixElements = {
            //       new float[] {0, 0, 0, 0, 0},        // red
            //       new float[] {0, 0, 0, 0, 0},        // green
            //       new float[] {0, 0, 0, 0, 0},        // blue
            //       new float[] {0, 0, 0, 1, 1},        // alpha
            //       new float[] {0, 0, 0, 0, 0} };    // translations

            //var matrix = new ColorMatrix(colorMatrixElements);

            //ImageAttributes attributes = new ImageAttributes();
            //attributes.SetColorMatrix(matrix);

            //var sourceAlpha = new Bitmap(source.Width, source.Height);

            //using (var graphics = Graphics.FromImage(sourceAlpha))
            //{

            //    graphics.DrawImage(source, new Rectangle(0, 0, source.Width, source.Height), 0, 0,
            //          source.Width, source.Height, GraphicsUnit.Pixel, attributes);
            //    graphics.Save();
            //}

            //return sourceAlpha;
        }



        bool ICollection<KeyValuePair<string, IDrawable>>.IsReadOnly
        {
            get { return false; }
        }
        ICollection<string> IDictionary<string, IDrawable>.Keys
        {
            get { return _images.Keys; }
        }
        ICollection<IDrawable> IDictionary<string, IDrawable>.Values
        {
            get { return _images.Values; }
        }

        void ICollection<KeyValuePair<string, IDrawable>>.Add(KeyValuePair<string, IDrawable> item)
        {
            _images.Add(item.Key, item.Value);
        }
        bool ICollection<KeyValuePair<string, IDrawable>>.Contains(KeyValuePair<string, IDrawable> item)
        {
            return ((IDictionary<string, IDrawable>)_images).Contains(item);
        }
        void ICollection<KeyValuePair<string, IDrawable>>.CopyTo(KeyValuePair<string, IDrawable>[] array, int arrayIndex)
        {
            ((IDictionary<string, IDrawable>)_images).CopyTo(array, arrayIndex);
        }
        bool ICollection<KeyValuePair<string, IDrawable>>.Remove(KeyValuePair<string, IDrawable> item)
        {
            return _images.Remove(item.Key);
        }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _images.GetEnumerator();
        }
    }
}
