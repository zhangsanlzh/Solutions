# Bitmap拼接-传统方法

```csharp
        #region 自定义方法，直接操作 Bitmap 合并，会有性能问题

        [Obsolete]
        public static Drawing.Bitmap CombineBitmap_H(string[] paths)
        {
            if (paths.Length <= 1)
            {
                throw new ArgumentException("Combining operation need two images at least.");
            }

            var img0 = new Drawing.Bitmap(paths[0]);
            Drawing.Bitmap imgNext, tmpImg = img0;

            for (int i = 1; i < paths.Length; i++)
            {
                imgNext = new Drawing.Bitmap(paths[i]);
                tmpImg = Merge_H(tmpImg, imgNext, PixelFormat.Format24bppRgb);
            }

            return tmpImg;
        }

        [Obsolete]
        public static Drawing.Bitmap CombineBitmap_V(string[] paths)
        {
            if (paths.Length <= 1)
            {
                throw new ArgumentException("Combining operation need two images at least.");
            }

            var img0 = new Drawing.Bitmap(paths[0]);
            Drawing.Bitmap imgNext, tmpImg = img0;

            for (int i = 1; i < paths.Length; i++)
            {
                imgNext = new Drawing.Bitmap(paths[i]);
                tmpImg = Merge_V(tmpImg, imgNext, PixelFormat.Format24bppRgb);
            }

            return tmpImg;
        }

        /// <summary>
        /// 将源图像灰度化，并转化为8位灰度图像。
        /// </summary>
        /// <param name="original"> 源图像。 </param>
        /// <returns> 8位灰度图像。 </returns>
        private Bitmap RgbToGrayScale(Bitmap original)
        {
            if (original != null)
            {
                // 将源图像内存区域锁定
                Rectangle rect = new Rectangle(0, 0, original.Width, original.Height);
                BitmapData bmpData = original.LockBits(rect, ImageLockMode.ReadOnly,
                        PixelFormat.Format24bppRgb);

                // 获取图像参数
                int width = bmpData.Width;
                int height = bmpData.Height;
                int stride = bmpData.Stride;  // 扫描线的宽度,比实际图片要大
                int offset = stride - width * 3;  // 显示宽度与扫描线宽度的间隙
                IntPtr ptr = bmpData.Scan0;   // 获取bmpData的内存起始位置的指针
                int scanBytesLength = stride * height;  // 用stride宽度，表示这是内存区域的大小

                // 分别设置两个位置指针，指向源数组和目标数组
                int posScan = 0, posDst = 0;
                byte[] rgbValues = new byte[scanBytesLength];  // 为目标数组分配内存
                Marshal.Copy(ptr, rgbValues, 0, scanBytesLength);  // 将图像数据拷贝到rgbValues中
                // 分配灰度数组
                byte[] grayValues = new byte[width * height]; // 不含未用空间。
                // 计算灰度数组

                byte blue, green, red;
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {

                        blue = rgbValues[posScan];
                        green = rgbValues[posScan + 1];
                        red = rgbValues[posScan + 2];
                        grayValues[posDst] = (byte)((blue + green + red) / 3);
                        posScan += 3;
                        posDst++;

                    }
                    // 跳过图像数据每行未用空间的字节，length = stride - width * bytePerPixel
                    posScan += offset;
                }

                // 内存解锁
                Marshal.Copy(rgbValues, 0, ptr, scanBytesLength);
                original.UnlockBits(bmpData);  // 解锁内存区域

                // 构建8位灰度位图
                Bitmap retBitmap = BuiltGrayBitmap(grayValues, width, height);
                return retBitmap;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 用灰度数组新建一个8位灰度图像。
        /// </summary>
        /// <param name="rawValues"> 灰度数组(length = width * height)。 </param>
        /// <param name="width"> 图像宽度。 </param>
        /// <param name="height"> 图像高度。 </param>
        /// <returns> 新建的8位灰度位图。 </returns>
        private Bitmap BuiltGrayBitmap(byte[] rawValues, int width, int height)
        {
            // 新建一个8位灰度位图，并锁定内存区域操作
            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format8bppIndexed);
            BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, width, height),
                 ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);

            // 计算图像参数
            int offset = bmpData.Stride - bmpData.Width;        // 计算每行未用空间字节数
            IntPtr ptr = bmpData.Scan0;                         // 获取首地址
            int scanBytes = bmpData.Stride * bmpData.Height;    // 图像字节数 = 扫描字节数 * 高度
            byte[] grayValues = new byte[scanBytes];            // 为图像数据分配内存

            // 为图像数据赋值
            int posSrc = 0, posScan = 0;                        // rawValues和grayValues的索引
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    grayValues[posScan++] = rawValues[posSrc++];
                }
                // 跳过图像数据每行未用空间的字节，length = stride - width * bytePerPixel
                posScan += offset;
            }

            // 内存解锁
            Marshal.Copy(grayValues, 0, ptr, scanBytes);
            bitmap.UnlockBits(bmpData);  // 解锁内存区域

            // 修改生成位图的索引表，从伪彩修改为灰度
            ColorPalette palette;
            // 获取一个Format8bppIndexed格式图像的Palette对象
            using (Bitmap bmp = new Bitmap(1, 1, PixelFormat.Format8bppIndexed))
            {
                palette = bmp.Palette;
            }
            for (int i = 0; i < 256; i++)
            {
                palette.Entries[i] = Color.FromArgb(i, i, i);
            }
            // 修改生成位图的索引表
            bitmap.Palette = palette;

            return bitmap;
        }

        private static Bitmap Merge_H(Bitmap left, Bitmap right, PixelFormat format)
        {
            if (!(format == PixelFormat.Format8bppIndexed || format == PixelFormat.Format24bppRgb))
            {
                throw new ArgumentException("Only 8 bit or 24 bit deep bmp image can be merge");
            }

            int n = 1;
            if (format == PixelFormat.Format24bppRgb)
            {
                n = 3;
            }

            Bitmap bmpOut = new Bitmap(left.Width + right.Width, left.Height);
            unsafe
            {
                BitmapData leftData = left.LockBits(new Rectangle(0, 0, left.Width, left.Height), ImageLockMode.ReadOnly, format);
                byte* leftPtr = (byte*)(void*)leftData.Scan0;

                BitmapData rightData = right.LockBits(new Rectangle(0, 0, right.Width, left.Height), ImageLockMode.ReadOnly, format);
                byte* rightPtr = (byte*)(void*)rightData.Scan0;

                BitmapData outData = bmpOut.LockBits(new Rectangle(0, 0, bmpOut.Width, bmpOut.Height), ImageLockMode.ReadWrite, format);
                byte* outPtr = (byte*)(void*)outData.Scan0;

                int outStride = outData.Stride;
                int offL = leftData.Stride - n * left.Width;
                int offR = rightData.Stride - n * right.Width;
                for (int y = 0; y < left.Height; ++y)
                {
                    for (int x = 0; x < leftData.Width * n; ++x)
                    {
                        outPtr[outStride * y + x] = leftPtr[0];
                        ++leftPtr;
                    }
                    leftPtr += offL;

                    for (int x = 0; x < rightData.Width * n; ++x)
                    {
                        outPtr[outStride * y + n * left.Width + x] = rightPtr[0];
                        ++rightPtr;
                    }
                    rightPtr += offR;
                }

                left.UnlockBits(leftData);
                right.UnlockBits(rightData);
                bmpOut.UnlockBits(outData);
                return bmpOut;
            }
        }

        private static Bitmap Merge_V(Bitmap bottom, Bitmap top, PixelFormat format)
        {
            if (!(format == PixelFormat.Format8bppIndexed || format == PixelFormat.Format24bppRgb))
            {
                throw new ArgumentException("Only 8 bit or 24 bit deep bmp image can be merge");
            }

            int n = 1;
            if (format == PixelFormat.Format24bppRgb)
            {
                n = 3;
            }

            Bitmap bmpOut = new Bitmap(bottom.Width, bottom.Height + top.Height);
            unsafe
            {
                BitmapData bottomData = bottom.LockBits(new Rectangle(0, 0, bottom.Width, bottom.Height), ImageLockMode.ReadOnly, format);
                byte* bottomPtr = (byte*)(void*)bottomData.Scan0;

                BitmapData topData = top.LockBits(new Rectangle(0, 0, top.Width, top.Height), ImageLockMode.ReadOnly, format);
                byte* topPtr = (byte*)(void*)topData.Scan0;

                BitmapData outData = bmpOut.LockBits(new Rectangle(0, 0, bmpOut.Width, bmpOut.Height), ImageLockMode.ReadWrite, format);
                byte* outPtr = (byte*)(void*)outData.Scan0;

                int outStride = outData.Stride;
                int offB = bottomData.Stride - n * bottom.Width;
                int offT = topData.Stride - n * top.Width;

                for (int y = 0; y < top.Height; ++y)
                {
                    for (int x = 0; x < topData.Width * n; ++x)
                    {
                        outPtr[outStride * y + x] = topPtr[0];
                        ++topPtr;
                    }
                    topPtr += offT;
                }

                for (int y = 0; y < bottom.Height; y++)
                {
                    for (int x = 0; x < bottomData.Width * n; ++x)
                    {
                        outPtr[outStride * y + n * top.Width * top.Height + x] = bottomPtr[0];
                        ++bottomPtr;
                    }
                    bottomPtr += offB;
                }

                bottom.UnlockBits(bottomData);
                top.UnlockBits(topData);
                bmpOut.UnlockBits(outData);
                return bmpOut;
            }
        }

        private Drawing.Bitmap CombineBitmap_H(Drawing.Bitmap first, Drawing.Bitmap second)
        {
            int width = first.Width + second.Width;
            int height = Math.Max(first.Height, second.Height);
            var newBitmap = new Drawing.Bitmap(first.Width + second.Width, height);
            for (var i = 0; i <= first.Width - 1; i++)
            {
                for (var j = 0; j <= first.Height - 1; j++)
                {
                    var c = first.GetPixel(i, j);
                    newBitmap.SetPixel(i, j, c);
                }
            }

            for (var i = 0; i <= second.Width - 1; i++)
            {
                for (var j = 0; j <= second.Height - 1; j++)
                {
                    var c = second.GetPixel(i, j);
                    newBitmap.SetPixel(i + first.Width, j, c);
                }
            }

            first?.Dispose();
            second?.Dispose();
            return newBitmap;
        }

        private Drawing.Bitmap CombineBitmap_V(Drawing.Bitmap first, Drawing.Bitmap second)
        {
            int height = first.Height + second.Height;
            int width = Math.Max(first.Width, second.Width);
            var newBitmap = new Drawing.Bitmap(width, height);
            for (var i = 0; i <= first.Width - 1; i++)
            {
                for (var j = 0; j <= first.Height - 1; j++)
                {
                    var c = first.GetPixel(i, j);
                    newBitmap.SetPixel(i, j, c);
                }
            }

            for (var i = 0; i <= second.Width - 1; i++)
            {
                for (var j = 0; j <= second.Height - 1; j++)
                {
                    var c = second.GetPixel(i, j);
                    newBitmap.SetPixel(i, j + first.Height, c);
                }
            }

            first?.Dispose();
            second?.Dispose();
            return newBitmap;
        }

        #endregion

            
                    #region Emgu.CV 方法

        public static Image<Bgr, byte> ConcatHV(string[][] paths)
        {
            var images = new List<Image<Bgr, byte>>();
            foreach (var hpath in paths)
            {
                images.Add(ConcatH(hpath));
            }

            Image<Bgr, byte> imgNext, tmpImg = images[0];
            for (int i = 1; i < images.Count; i++)
            {
                imgNext = images[i];
                CvInvoke.VConcat(imgNext.Mat, tmpImg.Mat, tmpImg.Mat);
            }

            return tmpImg;
        }

        public static Image<Bgr, byte> ConcatH(string[] paths)
        {
            if (paths.Length <= 1)
            {
                throw new ArgumentException("concatenating operation need two images at least.");
            }

            var img0 = new Image<Bgr, byte>(paths[0]);
            Image<Bgr, byte> imgNext, tmpImg = img0;

            for (int i = 1; i < paths.Length; i++)
            {
                imgNext = new Image<Bgr, byte>(paths[i]);
                CvInvoke.HConcat(tmpImg.Mat, imgNext.Mat, tmpImg.Mat);
            }

            return tmpImg;
        }

        public static Image<Bgr, byte> ConcatV(string[] paths)
        {
            if (paths.Length <= 1)
            {
                throw new ArgumentException("concatenating operation need two images at least.");
            }

            var img0 = new Image<Bgr, byte>(paths[0]);
            Image<Bgr, byte> imgNext, tmpImg = img0;

            for (int i = 1; i < paths.Length; i++)
            {
                imgNext = new Image<Bgr, byte>(paths[i]);
                CvInvoke.VConcat(imgNext.Mat, tmpImg.Mat, tmpImg.Mat);
            }

            return tmpImg;
        }

        #endregion

```

