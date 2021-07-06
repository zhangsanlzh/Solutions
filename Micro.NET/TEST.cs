/*************************************************************************************
  * CLR 版本：       4.0.30319.42000
  * 类 名 称：       FARecipeConverter
  * 命名空间：       UP.UPCF.Recipe.Common
  * 文 件 名：       FARecipeConverter
  * 创建时间：       2021/5/28 13:32:10
  * 作    者：             
  * 公    司：       u-precision  
  * 说    明：
  * 修改时间：
  * 修 改 人：      //只保留最新
 *************************************************************************************/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using UP.UPCF.Common;

namespace UP.UPCF.Recipe.Common
{
    public class FARecipeConverter
    {
        private DataSetHelper dataSetHelper = null;
        private RecipeHelper recipeHelper = new RecipeHelper();

        public FARecipeConverter(string moduleName, string recipeName)
        {
            ModuleName = moduleName;
            RecipeName = recipeName;
            recipeHelper.LoadRecipeData(moduleName, recipeName);
        }

        private string ModuleName
        {
            get; set;
        }

        private string RecipeName
        {
            get; set;
        }

        public void Convert(string extensionName)
        {
            var faRecipe = new FARecipe();
            faRecipe.ASCNodes.Add(Path.Combine(recipeHelper.Data.ModuleName, recipeHelper.Data.RecipeName + ".mrp"));
            faRecipe.ASCNodes.Add(recipeHelper.Data.RecipeName);
            faRecipe.ASCNodes.Add("1.00");

            var lstBody = new LSTBody();
            lstBody.ASCNode = "RecipeHeader";

            var format = new LSTItem();
            format.AddItem("RecipeFormat");
            format.AddItem("mrp");
            lstBody.Items.AddNode(format);

            var creator = new LSTItem();
            creator.AddItem("Creator");
            creator.AddItem(recipeHelper.Data.Creator);
            lstBody.Items.AddNode(creator);

            var creatTime = new LSTItem();
            creatTime.AddItem("CreateTime");
            creatTime.AddItem(recipeHelper.Data.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"));
            lstBody.Items.AddNode(creatTime);

            var editor = new LSTItem();
            editor.AddItem("LastModify");
            editor.AddItem(recipeHelper.Data.Editor);
            lstBody.Items.AddNode(editor);

            var editTime = new LSTItem();
            editTime.AddItem("LastModifyTime");
            editTime.AddItem(recipeHelper.Data.EditTime.ToString("yyyy-MM-dd HH:mm:ss"));
            lstBody.Items.AddNode(editTime);

            var description = new LSTItem();
            description.AddItem("Description");
            description.AddItem(recipeHelper.Data.Description);
            lstBody.Items.AddNode(description);

            faRecipe.Bodys.AddBody(lstBody);

            foreach (var step in recipeHelper.Data.Steps)
            {
                var lstStepBody = new LSTBody();
                lstStepBody.ASCNode = step.Name;

                step.Datas.ForEach(item =>
                {
                    var itemStep = new LSTItem();
                    itemStep.AddItem(item.Name);
                    itemStep.AddItem(item.Value);
                    lstStepBody.Items.AddNode(itemStep);
                });

                faRecipe.Bodys.AddBody(lstStepBody);
            }

            var helper = new XmlSerializerHelper<FARecipe>();
            //helper.IncludeMetaInfor = false;
            if (extensionName.Contains("."))
            {
                helper.SaveToFile(@"..\WaferFlow\" + ModuleName + "\\" + RecipeName + extensionName, faRecipe);
            }
            else
            {
                helper.SaveToFile(@"..\WaferFlow\" + ModuleName + "\\" + RecipeName + "." + extensionName, faRecipe);
            }
        }
    }

    [XmlRoot("LST")]
    public class FARecipe
    {
        private List<string> ascNodes = new List<string>();
        private BodyCollection bodys = new BodyCollection();

        [XmlElement(ElementName = "ASC")]
        public List<string> ASCNodes
        {
            get
            {
                return ascNodes;
            }

            set
            {
                ascNodes = value;
            }
        }

        [XmlElement(ElementName = "LST")]
        public BodyCollection Bodys
        {
            get
            {
                return bodys;
            }

            set
            {
                bodys = value;
            }
        }
    }

    public class BodyCollection
    {
        private List<LSTBody> bodys = new List<LSTBody>();

        [XmlElement(Type = typeof(LSTBody), ElementName = "LST")]
        public List<LSTBody> RecipeBody
        {
            get
            {
                return bodys;
            }

            set
            {
                bodys = value;
            }
        }

        public void AddBody(LSTBody body)
        {
            bodys.Add(body);
        }
    }

    public class LSTBody
    {
        private ItemCollection collections = new ItemCollection();

        [XmlElement(ElementName = "ASC")]
        public string ASCNode
        {
            get; set;
        }

        [XmlElement(ElementName = "LST")]
        public ItemCollection Items
        {
            get
            {
                return collections;
            }

            set
            {
                collections = value;
            }
        }
    }

    public class ItemCollection
    {
        private List<LSTItem> lstNodes = new List<LSTItem>();

        [XmlElement(ElementName = "LST", Type = typeof(LSTItem))]
        public List<LSTItem> LSTNodes
        {
            get
            {
                return lstNodes;
            }

            set
            {
                lstNodes = value;
            }
        }

        public void AddNode(LSTItem item)
        {
            lstNodes.Add(item);
        }
    }

    public class LSTItem
    {
        private List<string> ascNodes = new List<string>();

        [XmlElement(ElementName = "ASC")]
        public List<string> ASCNodes
        {
            get
            {
                return ascNodes;
            }

            set
            {
                ascNodes = value;
            }
        }

        public void AddItem(string node)
        {
            ascNodes.Add(node);
        }
    }
}
