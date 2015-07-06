using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Foundation;
using UIKit;
using Xamarin.Forms;
using XF_DataSaveSample.iOS;

[assembly: Dependency(typeof(SaveAndLoad_iOS))]

namespace XF_DataSaveSample.iOS
{
    class SaveAndLoad_iOS: ISaveAndLoad
    {
        public void SaveData(string filename, string text)
        {
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, filename);
            // File.WriteAllText ですべて上書きします。AppendAllText だと追加するようです。
            System.IO.File.WriteAllText(filePath, text);
        }
        public string LoadData(string filename)
        {
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var filePath = System.IO.Path.Combine(documentsPath, filename);
            // ファイルが無ければ null を返します。
            if (System.IO.File.Exists(filePath))
            {
                return System.IO.File.ReadAllText(filePath);
            }
            return null;
        }
        public bool ClearData(string filename)
        {
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var filePath = System.IO.Path.Combine(documentsPath, filename);
            System.IO.File.Delete(filePath);
            // ファイルが削除出来ていれば true, そうでなければ false を返します。
            return (System.IO.File.Exists(filePath)) ? false : true;
        }
    }
}