﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormularioAsistencia.Utilities
{
    public class ZipItem
    {
        public string Name { get; set; }
        public Stream Content { get; set; }
        public ZipItem(string name, byte[] byteArray)
        {
            var memoryStream = new MemoryStream(byteArray);
            this.Name = name;
            this.Content = memoryStream;
        }

        public ZipItem(string name, string contentStr, Encoding encoding)
        {
            // convert string to stream
            var byteArray = encoding.GetBytes(contentStr);
            var memoryStream = new MemoryStream(byteArray);
            this.Name = name;
            this.Content = memoryStream;
        }
    }
}
