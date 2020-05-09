using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace _3DTransform
{
    class Triangle3D
    {
        private Vector4 normal;
        public Vector4 a, b, c;
        public Vector4 A, B, C;
        private float dot;
        private bool cullBack;
        public Triangle3D() { }
        public Triangle3D(Vector4 a, Vector4 b, Vector4 c)
        {
            this.A = this.a = new Vector4(a);
            this.B = this.b = new Vector4(b);
            this.C = this.c = new Vector4(c);
        }
        //模型->世界->摄像机，结合律，在外部算出来再传进来,防止误差越来越大，每次用原始数据计算
        public void Transform(Matrix4x4 m)
        {
            this.a = m.Mul(this.A);
            this.b = m.Mul(this.B);
            this.c = m.Mul(this.C);
        }
        //模型->世界，光照
        public void CalculateLighting(Matrix4x4 m, Vector4 L)
        {
            this.Transform(m);

            Vector4 u = this.b - this.a;
            Vector4 v = this.c - this.a;
            Vector4 normal = u.Cross(v);

            dot = normal.Normalized.Dot(L.Normalized);
            dot = Math.Max(0, dot);

            Vector4 e = new Vector4(0, 0,-1,0); //视向量
            cullBack = normal.Normalized.Dot(e) < 0 ? true : false; //背面剔除
        }

        public void Draw(Graphics g)
        {
            //g.TranslateTransform(300, 300);
            //g.DrawLines(new Pen(Color.Black, 2), this.Get2DPointFarr());

            if (!cullBack)
            {
                GraphicsPath path = new GraphicsPath();
                path.AddLines(this.Get2DPointFarr());

                int r = (int)(255 * dot);
                Color color = Color.FromArgb(r, r, r);
                Brush br = new SolidBrush(color);
                g.FillPath(br, path);
            }

        }
        private PointF[] Get2DPointFarr()
        {
            PointF[] arr = new PointF[4];
            arr[0] = Get2DPointF(this.a);
            arr[1] = Get2DPointF(this.b);
            arr[2] = Get2DPointF(this.c);
            arr[3] = arr[0];
            return arr;
        }
        private PointF Get2DPointF(Vector4 v)
        {
            PointF p = new PointF();
            p.X = (float)(v.x / v.w);
            p.Y = (float)(v.y / v.w);
            return p;
        }
    }
}
