using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Web;
using System.Security.Permissions;
using System.Collections.Specialized;

namespace TygaSoft.CustomProvider
{
    [AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
    public class AssetSiteMapProvider : StaticSiteMapProvider
    {
        private string siteMapFile = null;
        private SiteMapNode rootNode = null;

        private bool initialized = false;
        public virtual bool IsInitialized
        {
            get
            {
                return initialized;
            }
        }

        public override SiteMapNode RootNode
        {
            get
            {
                SiteMapNode temp = null;
                temp = BuildSiteMap();
                return temp;
            }
        }

        public override void Initialize(string name, NameValueCollection attributes)
        {
            if (IsInitialized) return;

            base.Initialize(name, attributes);
            siteMapFile = attributes["siteMapFile"];
            initialized = true;
        }

        protected override void Clear()
        {
            lock (this)
            {
                rootNode = null;
                base.Clear();
            }
        }

        public override SiteMapNode BuildSiteMap()
        {
            lock (this)
            {
                if (null == rootNode)
                {
                    Clear();

                    var xel = XElement.Load(HttpContext.Current.Server.MapPath(siteMapFile));
                    var firstEle = xel.Descendants().FirstOrDefault();
                    if (firstEle == null) throw new ArgumentException("站点导航提供程序未正确配置");

                    rootNode = new SiteMapNode(this,
                                firstEle.Attribute("Id").Value,
                                firstEle.Attribute("Url").Value,
                                firstEle.Attribute("Title").Value,
                                firstEle.Attribute("Description").Value);

                    AddNode(rootNode);

                    CreateChildNode(xel, rootNode);
                }

                return rootNode;
            }
        }

        protected override SiteMapNode GetRootNodeCore()
        {
            return rootNode;
        }

        private void CreateChildNode(XElement xel, SiteMapNode parentNode)
        {
            var q = xel.Descendants().Where(x => x.Attribute("ParentId").Value == parentNode.Key);
            if (q != null && q.Count() > 0)
            {
                foreach (var item in q)
                {
                    var childNode = new SiteMapNode(this,
                        item.Attribute("Id").Value,
                        item.Attribute("Url").Value,
                        item.Attribute("Title").Value,
                        item.Attribute("Description").Value);
                    AddNode(childNode, parentNode);

                    CreateChildNode(xel, childNode);
                }
            }
        }
    }
}