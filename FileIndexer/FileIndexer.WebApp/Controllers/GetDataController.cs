using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FileIndexer.Data;
using FileIndexer.Data.Models;

namespace FileIndexer.WebApp.Controllers
{
    public class GetDataController : Controller
    {
        // GET: GetData
        private ITextFilesAndWordsRepo _repository;

        public GetDataController(ITextFilesAndWordsRepo repo)
        {
            _repository = repo;

        }
        public JsonResult GetFiles(int skip=0,int take=-1)
        {
            var result = new JsonResult()
            {
                Data = _repository.GetFiles(skip,take),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
            return result;
        }

    }
}