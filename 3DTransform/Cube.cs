using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace _3DTransform
{
    class Cube
    {
        Vector4 a = new Vector4(-0.5,0.5,0.5,1);
        Vector4 b = new Vector4(0.5, 0.5, 0.5, 1);
        Vector4 c = new Vector4(0.5, 0.5, -0.5, 1);
        Vector4 d = new Vector4(-0.5, 0.5, -0.5, 1);

        Vector4 e = new Vector4(-0.5, -0.5, 0.5, 1);
        Vector4 f = new Vector4(0.5, -0.5, 0.5, 1);
        Vector4 g = new Vector4(0.5, -0.5, -0.5, 1);
        Vector4 h = new Vector4(-0.5, -0.5, -0.5, 1);

        private Triangle3D[] triangeles = new Triangle3D[12];

        public Cube()
        {
            //top
            triangeles[0] = new Triangle3D(a,b,c);
            triangeles[1] = new Triangle3D(a, c, d);
            //bottom
            triangeles[2] = new Triangle3D(e, h, f);
            triangeles[3] = new Triangle3D(f, h, g);
            //front
            triangeles[4] = new Triangle3D(d, c, g);
            triangeles[5] = new Triangle3D(d, g, h);
            //back
            triangeles[6] = new Triangle3D(a, e, b);
            triangeles[7] = new Triangle3D(b, e, f);
            //right
            triangeles[8] = new Triangle3D(b, f, c);
            triangeles[9] = new Triangle3D(c, f, g);
            //left
            triangeles[10] = new Triangle3D(a,d,h);
            triangeles[11] = new Triangle3D(a,h,e);
        }

        public void Transform(Matrix4x4 m)
        {
            foreach (Triangle3D t in triangeles)
                t.Transform(m);
        }

        public void CalculateLighting(Matrix4x4 m, Vector4 L)
        {
            foreach (Triangle3D t in triangeles)
                t.CalculateLighting(m, L);
        }

        public void Draw(Graphics g)
        {
            g.TranslateTransform(300, 300);

            foreach (Triangle3D t in triangeles)
                t.Draw(g);
        }
    }
}
