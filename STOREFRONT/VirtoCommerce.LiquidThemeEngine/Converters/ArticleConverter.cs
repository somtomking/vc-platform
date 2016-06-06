﻿using Omu.ValueInjecter;
using System.Linq;
using VirtoCommerce.LiquidThemeEngine.Objects;
using VirtoCommerce.Storefront.Model.Common;
using StorefrontModel = VirtoCommerce.Storefront.Model.StaticContent;

namespace VirtoCommerce.LiquidThemeEngine.Converters
{
    public static class ArticleCOnverter
    {
        public static Article ToShopifyModel(this StorefrontModel.BlogArticle article)
        {
            var retVal = new Article();

            retVal.InjectFrom<NullableAndEnumValueInjecter>(article);
            retVal.Handle = article.Url;
            retVal.CreatedAt = article.CreatedDate;
            retVal.PublishedAt = article.PublishedDate ?? article.CreatedDate;
            retVal.ImageUrl = article.ImageUrl;
            retVal.Tags = article.Tags != null ? article.Tags.OrderBy(t => t).ToArray() : null;

            return retVal;
        }
    }
}