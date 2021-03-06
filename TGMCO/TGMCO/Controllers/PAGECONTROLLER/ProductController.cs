﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TGMCO.Models;
using TGMCO.Models.Entity;

namespace TGMCO.Controllers.PAGECONTROLLER
{
    public class ProductController : Controller
    {
        TGMCOEntitiesDB db = new TGMCOEntitiesDB();

        public ActionResult Detail(int id)
        {
            try
            {

                    SupplierModel _SUPPLIER = new SupplierModel();
                    PRODUCT _PRODUCT = new PRODUCT();
                    if (db.PRODUCTS.Find(id) != null)
                    {
                        Session["SUPPLIER"] = _SUPPLIER.GetSupplierName(db.PRODUCTS.Find(id).SUPPLIER_ID);
                        Session["SUPPLIER_MODEL"] = db.SUPPLIERS.Find(db.PRODUCTS.Find(id).SUPPLIER_ID);
                        _PRODUCT = db.PRODUCTS.Find(id);
                    }
                                 
                return View(_PRODUCT);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }          
        }
        public ActionResult Detail_Bosch(int id)
        {
            try
            {

                SupplierModel _SUPPLIER = new SupplierModel();
                Session["SUPPLIER"] = _SUPPLIER.GetSupplierName(db.PRODUCTS.Find(id).SUPPLIER_ID);
                Session["SUPPLIER_MODEL"] = db.SUPPLIERS.Find(db.PRODUCTS.Find(id).SUPPLIER_ID);

                PRODUCT _PRODUCT = db.PRODUCTS.Find(id);
                ViewBag.CategoryID = _PRODUCT.CATEGORY_ID;
                ViewBag.CategoryNAME = db.CATEGORIES.Single(n => n.CATEGORY_ID == _PRODUCT.CATEGORY_ID).CATEGORY_NAME;
                return View(_PRODUCT);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }
        }

        [HttpPost]
        public ActionResult Search(FormCollection f)
        {
            try
            {
                if(!string.IsNullOrEmpty(Session["SUPPLIER"].ToString()))
                {
                    Session["SUPPLIER"] = "DEFAULT";
                    Session["SUPPLIER_MODEL"] = db.SUPPLIERS.Find(20);
                }

                string key = f.Get("txtKeySearch").ToString().Trim();
                SUPPLIER _SUPPLIER = (SUPPLIER)Session["SUPPLIER_MODEL"];
                List<PRODUCT> _lstPRODUCT = db.PRODUCTS.Where(n => (n.PRODUCT_CODE.Contains(key) || n.PRODUCT_NAME.Contains(key))).ToList();
                ViewBag.KeySearch = key;
                ViewBag.NumProduct = _lstPRODUCT.Count;
                ViewBag.ListSupplier = new SelectList(db.SUPPLIERS.OrderByDescending(n => n.SUPPLIER_ID).ToList(), "Supplier_ID", "SUPPLIER_NAME");
                ViewBag.ListCategory = new SelectList(db.CATEGORIES.OrderByDescending(n => n.CATEGORY_ID).ToList(), "Category_ID", "CATEGORY_NAME");
                Session["KeySearch"] = key;
                return View(_lstPRODUCT);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }
        }
        [HttpGet]
        public ActionResult Search()
        {
            try
            {
                if (!string.IsNullOrEmpty(Session["SUPPLIER"].ToString()))
                {
                    Session["SUPPLIER"] = "DEFAULT";
                    Session["SUPPLIER_MODEL"] = db.SUPPLIERS.Find(20);
                }

                string key = (string)Session["KeySearch"];
                SUPPLIER _SUPPLIER = (SUPPLIER)Session["SUPPLIER_MODEL"];
                List<PRODUCT> _lstPRODUCT = db.PRODUCTS.Where(n => (n.PRODUCT_CODE.Contains(key) || n.PRODUCT_NAME.Contains(key)) && n.SUPPLIER_ID == _SUPPLIER.SUPPLIER_ID).ToList();
                ViewBag.KeySearch = key;
                ViewBag.NumProduct = _lstPRODUCT.Count;
                ViewBag.ListSupplier = new SelectList(db.SUPPLIERS.OrderByDescending(n => n.SUPPLIER_ID).ToList(), "Supplier_ID", "SUPPLIER_NAME");
                ViewBag.ListCategory = new SelectList(db.CATEGORIES.OrderByDescending(n => n.CATEGORY_ID).ToList(), "Category_ID", "CATEGORY_NAME");
                return View(_lstPRODUCT);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }
        }

        [HttpPost]
        public ActionResult AdvanceSearch(FormCollection f)
        {
            try
            {
                if (!string.IsNullOrEmpty(Session["SUPPLIER"].ToString()))
                {
                    Session["SUPPLIER"] = "DEFAULT";
                    Session["SUPPLIER_MODEL"] = db.SUPPLIERS.Find(20);
                }
                string key = f.Get("txtKeyWordAS").ToString().Trim();
                int supplier_id = 0;
                int category_id = 0;
                if(!string.IsNullOrEmpty(f.Get("ListSupplier").ToString()))
                {
                    supplier_id = int.Parse(f.Get("ListSupplier").ToString());
                    ViewBag.Supplier = db.SUPPLIERS.Find(supplier_id).SUPPLIER_NAME;
                }
                if (!string.IsNullOrEmpty(f.Get("ListCategory").ToString()))
                {
                    category_id = int.Parse(f.Get("ListCategory").ToString());
                    ViewBag.Category = db.CATEGORIES.Find(category_id).CATEGORY_NAME;
                }
                
                decimal min_price = decimal.Parse(f.Get("MinPrice").ToString());
                decimal max_price = decimal.Parse(f.Get("MaxPrice").ToString());

                SUPPLIER _SUPPLIER = (SUPPLIER)Session["SUPPLIER_MODEL"];
                List<PRODUCT> _lstPRODUCT = db.PRODUCTS.Where(n => (n.PRODUCT_CODE.Contains(key) || n.PRODUCT_NAME.Contains(key)) 
                                                                && ((n.SUPPLIER_ID == supplier_id && supplier_id != 0) || supplier_id == 0)
                                                                && ((n.CATEGORY_ID == category_id && category_id != 0) || category_id == 0)
                                                                && (n.UNIT_PRICE >= min_price)
                                                                && (n.UNIT_PRICE <= max_price)).ToList();
                ViewBag.KeySearch = key;
                ViewBag.NumProduct = _lstPRODUCT.Count;
                ViewBag.MinPrice = min_price;
                ViewBag.MaxPrice = max_price;
                ViewBag.ListSupplier = new SelectList(db.SUPPLIERS.OrderByDescending(n => n.SUPPLIER_ID).ToList(), "Supplier_ID", "SUPPLIER_NAME");
                ViewBag.ListCategory = new SelectList(db.CATEGORIES.OrderByDescending(n => n.CATEGORY_ID).ToList(), "Category_ID", "CATEGORY_NAME");
                return View("Search",_lstPRODUCT);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }
        }

    }
}
