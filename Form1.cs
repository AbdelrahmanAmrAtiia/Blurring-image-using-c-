using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;


namespace Bluring_Algorithm
{
    
    public partial class Form1 : Form
    {
       
        public Form1()
        {
            InitializeComponent();
            this.Load += new EventHandler(Form1_Load);
        }

        void Form1_Load(object sender, EventArgs e)
        {
            DirectoryInfo d = new DirectoryInfo(@"~~~~~~~~\"); //path of source folder that contain of not blured images
            FileInfo[] files = d.GetFiles("*.jpg"); //extension of images in path
            string str = "";

            foreach (FileInfo file in files) //looping on all image in a directory 
            {
                Bitmap image = new Bitmap(Image.FromFile(file.FullName)); // my image that want to make a process on it

                // window size(size of filter ) that also detect the degree of bluring
                int winsize = 4;

                Bitmap new_image = new Bitmap(image.Width, image.Height); // new image (blured image)

                for (int i = 0; i < image.Width; i++)
                {
                    for (int j = 0; j < image.Height; j++)
                    {
                        if (i > 200 && j > 200) // here i want to start bluring after 200 pixel from width (i) and 200 pixel from height (j)
                        {
                            double[] total = new double[3];

                            int count = 0;
                            for (int i1 = i - winsize; i1 <= i + winsize; i1++)
                            {
                                for (int j1 = j - winsize; j1 <= j + winsize; j1++)
                                {
                                    if (i1 >= 0 && j1 >= 0 && i1 < image.Width && j1 < image.Height)
                                    {
                                        System.Drawing.Color color = image.GetPixel(i1, j1);

                                        total[0] += color.R;
                                        total[1] += color.G;
                                        total[2] += color.B;

                                        count++;
                                    }


                                }
                            }

                            total[0] = total[0] / count;
                            total[1] = total[1] / count;
                            total[2] = total[2] / count;

                            new_image.SetPixel(i, j, System.Drawing.Color.FromArgb(255, (int)Math.Round(total[0]), (int)Math.Round(total[1]), (int)Math.Round(total[2])));

                        }
                        else // here copy other pixels from main image to my new image without blured (pixels). pixels from 0 -> 200
                        {
                            System.Drawing.Color color = image.GetPixel(i, j);
                            double[] total = new double[3];
                             total[0] += color.R;
                             total[1] += color.G;
                             total[2] += color.B;
                             new_image.SetPixel(i, j, System.Drawing.Color.FromArgb(255, (int)Math.Round(total[0]), (int)Math.Round(total[1]), (int)Math.Round(total[2])));

                        }
                        
                    }
                }

                //image = new_image;
                pictureBox1.Image=new_image;
                str = file.Name;
                new_image.Save(@"~~~~~~~\" + str + ".jpg"); //here save blured image in a new local path 


                
            }

        }
     
    }
}
