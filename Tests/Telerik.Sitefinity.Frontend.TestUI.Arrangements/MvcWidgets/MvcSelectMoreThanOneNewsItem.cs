﻿using System;
using System.IO;
using Telerik.Sitefinity.Frontend.TestUI.Arrangements.MvcWidgets;
using Telerik.Sitefinity.Frontend.TestUtilities;
using Telerik.Sitefinity.Frontend.TestUtilities.CommonOperations;
using Telerik.Sitefinity.TestArrangementService.Attributes;
using Telerik.Sitefinity.TestUI.Arrangements.Framework;
using Telerik.Sitefinity.TestUtilities.CommonOperations;
using MvcServerOperations = Telerik.Sitefinity.Mvc.TestUtilities.CommonOperations.ServerOperations;

namespace Telerik.Sitefinity.Frontend.TestUI.Arrangements
{
    /// <summary>
    /// MvcSelectMoreThanOneNewsItem arragement.
    /// </summary>
    public class MvcSelectMoreThanOneNewsItem : ITestArrangement
    {
        [ServerSetUp]
        public void SetUp()
        {
            for (int i = 0; i < 20; i++)
            {
                ServerOperations.News().CreatePublishedNewsItem(newsTitle: NewsItemTitle + i, newsContent: NewsItemContent + i, author: NewsItemAuthor + i);
            }

            Guid pageId = ServerOperations.Pages().CreatePage(PageName);

            FeatherServerOperations.ResourcePackages().ImportDataForSelectorsTests(FileResource, DesignerViewFileName, FileResourceJson, JsonFileName, ControllerFileResource, ControllerFileName); 

            MvcServerOperations.Widgets().AddMvcWidgetToPage(pageId, typeof(DummyTextController).FullName, WidgetCaption);
        }

        [ServerTearDown]
        public void TearDown()
        {
            ServerOperations.Pages().DeleteAllPages();
            ServerOperations.News().DeleteAllNews();

            FeatherServerOperations.ResourcePackages().DeleteSelectorsData(DesignerViewFileName, JsonFileName, ControllerFileName);
        }

        private const string FileResource = "Telerik.Sitefinity.Frontend.TestUI.Arrangements.Data.DesignerView.Selector.cshtml";
        private const string FileResourceJson = "Telerik.Sitefinity.Frontend.TestUI.Arrangements.Data.DesignerView.Selector.json";
        private const string ControllerFileResource = "Telerik.Sitefinity.Frontend.TestUI.Arrangements.Data.designerview-selector.js";

        private const string DesignerViewFileName = "DesignerView.Selector.cshtml";
        private const string JsonFileName = "DesignerView.Selector.json";
        private const string ControllerFileName = "designerview-selector.js";

        private const string PageName = "FeatherPage";
        private const string WidgetCaption = "SelectorWidget";

        private const string NewsItemTitle = "News Item Title";
        private const string NewsItemContent = "This is a news item.";
        private const string NewsItemAuthor = "NewsWriter";
    }
}
