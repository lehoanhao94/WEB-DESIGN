﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TGMCO.Models;

namespace TGMCO.Controllers.PAGECONTROLLER
{
    public class CategoryController : Controller
    {
        TGMCOEntitiesDB db = new TGMCOEntitiesDB();

        public ViewResult Category(int supplier_id, string supplier_code, int cate_id)
        {
            try
            {
                List<PRODUCT> _lstPRODUCT = db.PRODUCTS.Where(n => n.SUPPLIER_ID == supplier_id && n.CATEGORY_ID == cate_id).ToList();
                ViewBag.CategoryName = db.CATEGORIES.Single(n => n.CATEGORY_ID == cate_id).CATEGORY_NAME;
                return View(_lstPRODUCT);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 404;
                return null;
            }
        }


    }
}