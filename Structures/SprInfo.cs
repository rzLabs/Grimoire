using System.Drawing;

namespace Grimoire.Structures
{
    public class SprInfo
    {
        public SprInfo(string name)
        {
            Name = name;
            FrameCount = 1;
            Frames = new string[1] { $"{name}.jpg" };
            size = new Size(0, 0);
        }

        public string Name;
        public int FrameCount;
        public string[] Frames;

        Size size;

        public int Height
        {
            get
            {
                if (size != null)
                    return size.Height;

                return 0;
            }
        }

        public int Width
        {
            get
            {
                if (size != null)
                    return size.Width;

                return 0;
            }
        }

        public int Unused => 0;

        public void SetSize(int height, int width)
        {
            size.Height = height;
            size.Width = width;
        }

        public void SetSize(Size size) => this.size = size;
    }
}
