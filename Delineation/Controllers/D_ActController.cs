using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Delineation.Models;
using GemBox.Document;
using GemBox.Document.Tables;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Net;
using NPetrovich;

namespace Delineation.Controllers
{
    public class D_ActController : Controller
    {
        private readonly DelineationContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private string ConnStringOracle = "Data Source=//10.181.64.7/orcl7;User Id = sel;Password=2222";
        private string ConnStringSql = "Server=Pirr2n; database=pinskbase; User Id = ppinsk; Password=pes";

        public D_ActController(DelineationContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Drawing(int? id )
        {
            List<string> list_svg = new List<string>();
            if (id != null)
            {
                string path_svg = _webHostEnvironment.WebRootPath + "\\Output\\svg\\" + id.ToString() + ".svg";
                FileInfo MySvg = new FileInfo(path_svg);
                if (MySvg.Exists)
                {
                    using (StreamReader sr = new StreamReader(path_svg, System.Text.Encoding.Default))
                    {
                        string line;
                        bool content = false;
                        while ((line = sr.ReadLine()) != null)
                        {
                            if (line.IndexOf("konec") > -1)
                            {
                                content = false;
                                list_svg.Add(line.Substring(0, line.IndexOf("<rect id=\"konec\"")));
                            }
                            if (content) list_svg.Add(line);
                            if (line.IndexOf("nachalo") > -1) content = true;
                        }
                    }
                }
                ViewBag.list_svg = list_svg;
                string path = _webHostEnvironment.WebRootPath + "\\Output\\images\\";
                DirectoryInfo dir_image = new DirectoryInfo(path);
                List<FileInfo> list_files = dir_image.EnumerateFiles().Where(p => p.Name.Split('.')[0] == id.ToString()).ToList();
                foreach (FileInfo fileInfo in list_files)
                {
                    if (fileInfo.Exists) ViewBag.FileName = fileInfo.Name;
                }
                D_Act d_Act = await _context.d_Acts.Include(p => p.Tc).FirstOrDefaultAsync(o => o.Id == id);
                return View(d_Act);
            }
            else
            {
                return NotFound();
            }
        }
        /*[HttpPost]
        public async Task<IActionResult>  Drawing(List<IFormFile> postedFiles, int id, int del)
        {
            string log = "";
            string path = _webHostEnvironment.WebRootPath + "\\Output\\images\\";
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            if (del == 0)
            {
                foreach (IFormFile postedFile in postedFiles)
                {
                    string fileName1 = Path.GetFileName(postedFile.FileName);
                    string ext = fileName1.Split('.')[1];
                    string fileName = id + "." + ext;
                    var allowedExt = new[] { "png", "jpg", "jpeg" };
                    if (allowedExt.Any(p => p == ext.ToLower())) // проверка расширения
                    {
                        using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                        {
                            DeleteAllImageById(id,ext);
                            log += Path.Combine(path, fileName) + "/";
                            postedFile.CopyTo(stream);
                            ViewBag.FileName = fileName;
                            ViewBag.Message += string.Format("<b>{0}</b> загружен.<br />", fileName1);
                        }
                    }
                }
                ViewBag.xx = log;
            }
            else
            {
                DeleteAllImageById(id,"");
            }            
            D_Act d_Act = await _context.d_Acts.Include(p => p.Tc).FirstOrDefaultAsync(o => o.Id == id);
            return View(d_Act);
        }*/
        private void DeleteAllImageById(int id,string ext)
        {
            string path = _webHostEnvironment.WebRootPath + "\\Output\\images\\";
            DirectoryInfo dir_image = new DirectoryInfo(path);
            List<FileInfo> list_files = dir_image.EnumerateFiles().Where(p => p.Name.Split('.')[0] == id.ToString() && p.Name.Split('.')[1]!= ext).ToList();
            foreach (FileInfo fileInfo in list_files)
            {
                if (fileInfo.Exists) fileInfo.Delete();
            }
            //List<string> list_ext0 = new List<string>( new string[] { "png", "jpg", "jpeg" });
            //List<string> list_ext = new List<string> { ".png", ".jpg", ".jpeg" };
        }
        [HttpPost]
        //[RequestSizeLimit(40000000)]
        public IActionResult SavePNG(string png, string svg, string raw, int id)
        {
            var decodeURL_svg = WebUtility.UrlDecode(svg);
            var base64Data_svg = decodeURL_svg.Split(',');
            string path_svg = _webHostEnvironment.WebRootPath + "\\Output\\svg\\"+id.ToString()+".svg";
            using (FileStream fs = new FileStream(path_svg, FileMode.Create))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    byte[] data = Convert.FromBase64String(base64Data_svg[1]);
                    bw.Write(data);
                }
            }
            //---
            var decodeURL_png = WebUtility.UrlDecode(png);
            var base64Data_png = decodeURL_png.Split(',');
            string path_png = _webHostEnvironment.WebRootPath + "\\Output\\png\\" + id.ToString() + ".png";
            using (FileStream fs = new FileStream(path_png, FileMode.Create))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    byte[] data = Convert.FromBase64String(base64Data_png[1]);
                    bw.Write(data);
                }
            }
            //---
            if (raw != "0" && raw != "1") // 0 - удаление; 1- без изменений(картинка существует); иначе - пишем в поток
            {
                var decodeURL_raw = WebUtility.UrlDecode(raw);
                var base64Data_raw = decodeURL_raw.Split(',');
                string ext = base64Data_raw[0].Split('/')[1].Split(';')[0];
                string path_raw = _webHostEnvironment.WebRootPath + "\\Output\\images\\" + id.ToString() + "." + ext;
                var allowedExt = new[] { "png", "jpg", "jpeg" };
                if (allowedExt.Any(p => p == ext.ToLower())) // проверка расширения
                {
                    using (FileStream fs = new FileStream(path_raw, FileMode.Create))
                    {
                        using (BinaryWriter bw = new BinaryWriter(fs))
                        {
                            DeleteAllImageById(id, ext);
                            byte[] data = Convert.FromBase64String(base64Data_raw[1]);
                            bw.Write(data);
                        }
                    }
                }
            }
            else { if(raw == "0") DeleteAllImageById(id, "0"); }
            //---
            //D_Act d_Act = _context.d_Acts.Include(p => p.Tc).FirstOrDefault(o => o.Id == id);
            var d_Act = _context.d_Acts.Include(p => p.Tc).ThenInclude(p => p.Res).FirstOrDefault(p => p.Id == id);
            return RedirectToAction(nameof(Details),d_Act);
        }
        // GET: D_Act
        public async Task<IActionResult> Index()
        {
            string path_dir_pdf = _webHostEnvironment.WebRootPath + "\\Output\\pdf";
            DirectoryInfo directory = new DirectoryInfo(path_dir_pdf);
            List<string> fileNames = directory.GetFiles().Select(p=>p.Name.Split('.')[0]).ToList();
            ViewBag.fileNames = fileNames;
            var delineationContext = _context.d_Acts.Include(d => d.Tc).ThenInclude(p => p.Res).ThenInclude(p=>p.Nach).Where(p => p.State == (int)Stat.Completed);
            return View(await delineationContext.ToListAsync());
        }
        public async Task<IActionResult> Ind_agree()
        {
            var delineationContext = _context.d_Acts.Include(d => d.Tc).ThenInclude(p => p.Res).ThenInclude(p => p.Nach).Where(p => p.State == (int)Stat.Agreement);
            return View(await delineationContext.ToListAsync());
        }
        public async Task<IActionResult> Ind_edit()
        {
            var delineationContext = _context.d_Acts.Include(d => d.Tc).ThenInclude(p => p.Res).ThenInclude(p => p.Nach).Where(p=>p.State==(int)Stat.Edit);
            return View(await delineationContext.ToListAsync());
        }
        // GET: D_Act/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                string path_svg = _webHostEnvironment.WebRootPath + "\\Output\\png\\" + id.ToString() + ".png";
                FileInfo MyPng = new FileInfo(path_svg);
                if (MyPng.Exists) { ViewBag.fileName = MyPng.Name; }
            }
            var d_Act = await _context.d_Acts
                .Include(d => d.Tc)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (d_Act == null)
            {
                return NotFound();
            }
                return View(d_Act);
        }

        // GET: D_Act/Create
        public IActionResult Create()
        {
            List<SelList> myList = new List<SelList>();
            using (SqlConnection con = new SqlConnection(ConnStringSql))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = "select dbo.tu_all.kluch,n_tu,CONVERT(VARCHAR(10),d_tu,104) s2,fio,adress_ob,naim from dbo.tu_all,dbo.sprpodr Where del=1 and n_tu is not null and d_tu is not null and  CAST(kod AS NVARCHAR)+CAST(KOD_DOP AS NVARCHAR)=kod_podr and kod_podr='542000'";
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        myList.Add(new SelList() { Id = reader["kluch"].ToString(), Text = "№" + reader["n_tu"].ToString() + " от " + reader["s2"].ToString() + "; " + reader["fio"].ToString() + "; " + reader["adress_ob"].ToString() + "; " + reader["naim"].ToString() });
                    }
                    reader.Dispose();
                }
            }
            ViewData["TcId"] = new SelectList(myList, "Id", "Text");
            //ViewData["TcId"] = new SelectList(_context.D_Tces.OrderBy(p => p.Date).Select(p => new { Id = p.Id, text = "№" + p.Num + " от " + p.Date.ToString("dd.MM.yyyy") + "; " + p.FIO + "; " + p.Address }), "Id", "text");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int TcId)
        {
            string str_numTP = "",str="";
            D_Tc tc = new D_Tc();
            using (SqlConnection con = new SqlConnection(ConnStringSql))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    con.Open();
                    cmd.CommandText = "select kluch,n_tu, d_tu,kod_podr,fio,name_ob,adress_ob,p_all,name_ps,n_tp,typ_tp,n_vl,typ_vl,n_op,p_kat1,p_kat2,p_kat3 from dbo.tu_all where kluch=" + TcId.ToString();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        tc.Id =Convert.ToInt32(reader["kluch"]);
                        tc.Num = reader["n_tu"].ToString();
                        if(reader["d_tu"].ToString().Length >= 10) tc.Date = DateTime.Parse(reader["d_tu"].ToString());
                        tc.ResId = Convert.ToInt32(reader["kod_podr"]);
                        tc.FIO = reader["fio"].ToString();
                        tc.ObjName = reader["name_ob"].ToString();
                        tc.Address = reader["adress_ob"].ToString();
                        tc.Pow = reader["p_all"].ToString();
                        tc.PS = reader["name_ps"].ToString();
                        tc.TPnum = Convert.ToInt32(reader["n_tp"]);
                        tc.TP = reader["typ_tp"].ToString() + "-" + reader["n_tp"].ToString();
                        tc.Line04 = reader["typ_vl"].ToString() + " " + reader["n_vl"].ToString();
                        tc.Pillar = reader["n_op"].ToString();
                        str_numTP = reader["n_tp"].ToString();
                        string cat = "";
                        for(int i=1;i<4;i++) // если значение в одной из категорий >0, номер соответствующей категории записывается в tc.Category через запятую
                            if (Convert.ToDecimal(reader["p_kat" + i.ToString()]) > 0) cat += i.ToString() + ",";
                        int ilength = cat.Length - 1;
                        tc.Category = cat.Substring(0, ilength);
                    }
                    reader.Dispose();
                }
            }
            if (str_numTP != "") // обращение к базе Диполя
            {
                string path_vsd = _webHostEnvironment.WebRootPath + "\\Output\\vsd\\" + str_numTP + ".vsd";
                using (OracleConnection con = new OracleConnection(ConnStringOracle))
                {
                    string doc_code = "";
                    using (OracleCommand cmd = con.CreateCommand())
                    {
                        con.Open();
                        cmd.CommandText = "select TP_NUM, doc_code, type_txt from TP,TP_SYS_TYPES where substation_type_id=id and substr(doc_code,TO_NUMBER(INSTR(doc_code,'-',1,2))+1)=" + str_numTP;
                        OracleDataReader MyReader = cmd.ExecuteReader();
                        using (MyReader)
                        {
                            while (MyReader.Read())
                            {
                                //tc.TP += ":" + MyReader["type_txt"].ToString().Replace("ЗТП","ТП")+ "-" +str_numTP + " (" + MyReader["TP_NUM"].ToString() +")";
                                doc_code = MyReader["doc_code"].ToString();
                            }
                        }
                        cmd.CommandText = "select INV_NUMB from TP_INV_NUMB where doc_code='" + doc_code + "'";
                        MyReader = cmd.ExecuteReader();
                        using (MyReader)
                        {
                            while (MyReader.Read())
                            {
                                tc.TPInvNum = Convert.ToInt32(MyReader["inv_numb"]);
                            }
                        }
                        cmd.CommandText = "select line_doc_code, substation_id, vl10.substation_id as psid, p_name, p_voltage from TP_VL_10, vl10, psubstations where p_code=substation_id and vl10.doc_code=line_doc_code and substr(TP_VL_10.doc_code,TO_NUMBER(INSTR(TP_VL_10.doc_code,'-',1,2))+1)=" + str_numTP;
                        MyReader = cmd.ExecuteReader();
                        using (MyReader)
                        {
                            while (MyReader.Read())
                            {
                                str += "ВЛ 10кВ №" + MyReader["line_doc_code"].ToString().Split('-')[2] + ";" + "ПС " + MyReader["p_name"].ToString() + "-" + MyReader["p_voltage"].ToString() + "/";
                            }
                        }
                        FileInfo file_vsd = new FileInfo(path_vsd);
                        if (!file_vsd.Exists)
                        {
                            cmd.CommandText = "select PFILE, DOC_CODE from TP_SHEM WHERE DOC_CODE='" + doc_code + "'";
                            OracleDataReader MyReaderB = cmd.ExecuteReader();
                            using (MyReaderB)
                            {
                                while (MyReaderB.Read())
                                {
                                    OracleBinary oracleBinary = MyReaderB.GetOracleBinary(0);
                                    using (FileStream fstream = new FileStream(path_vsd, FileMode.OpenOrCreate))
                                    {
                                        byte[] array = (byte[])oracleBinary;
                                        fstream.Write(array, 0, array.Length);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            _context.D_Tces.Add(tc);
            _context.d_Acts.Add(new D_Act { Tc=tc , StrPSline10=str.Substring(0,str.Length-1)});
            _context.SaveChanges();
            D_Act act = _context.D_Act.ToList().LastOrDefault(p => p.TcId == TcId);
            return RedirectToAction(nameof(Edit), act);
        }

        // GET: D_Act/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var d_Act = await _context.d_Acts.Include(p=>p.Tc).ThenInclude(p=>p.Res).FirstOrDefaultAsync(p=>p.Id==id);
            if (d_Act == null)
            {
                return NotFound();
            }
            /*ViewData["TcId"] = new SelectList(_context.D_Tces.OrderBy(p=>p.Date).Select(p=>new { Id = p.Id, text = p.Num + " от " + p.Date.ToString("dd.MM.yyyy") + "; " + p.FIO + "; " + p.Address }), "Id", "text", d_Act.TcId);*/
            return View(d_Act);
        }

        // POST: D_Act/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string FIOcons, [Bind("Id,Date,TcId,IsEntity,EntityDoc,ConsBalance,DevBalance,ConsExpl,DevExpl,IsTransit,FIOtrans,Validity,Temp,StrPSline10")] D_Act d_Act)
        {
            if (id != d_Act.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    D_Tc d_Tc = _context.D_Tces.FirstOrDefault(p => p.Id == d_Act.TcId);
                    d_Tc.Line10 = d_Act.Temp.Split(';')[0];
                    d_Tc.PS = d_Act.Temp.Split(';')[1];
                    d_Tc.FIO = FIOcons;
                    _context.Update(d_Act);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!D_ActExists(d_Act.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                var d_Act2 = _context.d_Acts.Include(p => p.Tc).ThenInclude(p => p.Res).FirstOrDefault(p => p.Id == id);
                return RedirectToAction(nameof(Drawing),d_Act2);
            }
            /*ViewData["TcId"] = new SelectList(_context.D_Tces.OrderBy(p=>p.Date).Select(p=>new { Id = p.Id, text = p.Num + " от " + p.Date.ToString("dd.MM.yyyy") + "; " + p.FIO + "; " + p.Address }), "Id", "text", d_Act.TcId);*/
            return View(d_Act);
        }

        // GET: D_Act/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var d_Act = await _context.d_Acts
                .Include(d => d.Tc)
                .ThenInclude(l=>l.Res)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (d_Act == null)
            {
                return NotFound();
            }

            return View(d_Act);
        }

        // POST: D_Act/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            D_Act d_Act = await _context.d_Acts.FindAsync(id);
            D_Tc d_Tc = await _context.D_Tces.FindAsync(d_Act.TcId);
            _context.d_Acts.Remove(d_Act);
            _context.D_Tces.Remove(d_Tc);
            await _context.SaveChangesAsync();
            //
            string webRootPath = _webHostEnvironment.WebRootPath;
            string path_docx = webRootPath + "\\Output\\docx\\" + id + ".docx";
            string path_html = webRootPath + "\\Output\\html\\" + id + ".html";
            string path_html_folder = webRootPath + "\\Output\\html\\" + id + "_files";
            string path_pdf = webRootPath + "\\Output\\pdf\\" + id + ".pdf";
            string path_png = webRootPath + "\\Output\\png\\" + id + ".png";
            string path_svg = webRootPath + "\\Output\\svg\\" + id + ".svg";
            FileInfo docxToDel = new FileInfo(path_docx);
            if (docxToDel.Exists) docxToDel.Delete();
            FileInfo htmlToDel = new FileInfo(path_html);
            if (htmlToDel.Exists) htmlToDel.Delete();
            DirectoryInfo DirHtmlDel = new DirectoryInfo(path_html_folder);
            if (DirHtmlDel.Exists) DirHtmlDel.Delete(true);
            FileInfo pdfToDel = new FileInfo(path_pdf);
            if (pdfToDel.Exists) pdfToDel.Delete();
            FileInfo pngToDel = new FileInfo(path_png);
            if (pngToDel.Exists) pngToDel.Delete();
            FileInfo svgToDel = new FileInfo(path_svg);
            if (svgToDel.Exists) svgToDel.Delete();
            DeleteAllImageById(id, "0");
            return RedirectToAction(nameof(Ind_edit));
        }

        private bool D_ActExists(int id)
        {
            return _context.d_Acts.Any(e => e.Id == id);
        }
        public async Task<IActionResult> CreateAct(int? id, string type)
        {
            _context.D_Persons.Load();
            D_Act act = _context.D_Act.Include(p => p.Tc).ThenInclude(o => o.Res).Where(i => i.Id == id).FirstOrDefault();
            CreateDoc(act,type);
            if (type == "agreement")
                return RedirectToAction(nameof(Ind_edit));
            else
                return RedirectToAction(nameof(Index));
        }
        private void CreateDoc(D_Act act,string type)
        {
            bool IsEntity = act.IsEntity, IsTransit = act.IsTransit;
            string str_b = "", str_e = "";
            string str_Entity = IsEntity ? "Юридическое" : "Физическое";
            string str_ConsDover = IsEntity ? "действующ. на основании " + act.EntityDoc + " ": "";
            string str_Id = act.Id.ToString(),
                    str_City = str_b + act.Tc.Res.City + str_e,
                    str_RESa = str_b + act.Tc.Res.RESa + str_e,
                    str_RESom = str_b + act.Tc.Res.RESom + str_e,
                    str_FIOnachRod = str_b + act.Tc.Res.FIOnachRod + str_e,
                    str_Dover = str_b + act.Tc.Res.Dover + str_e,
                    str_DateAct = str_b + act.Date.ToString("dd.MM.yyyy") + str_e,
                    str_EntityDoc = str_b + act.EntityDoc + str_e,
                    str_NumTc = str_b + act.Tc.Num + str_e,
                    str_DateTc = str_b + act.Tc.Date.ToString("dd.MM.yyyy") + str_e,
                    str_RES = str_b + act.Tc.Res.Name + str_e,
                    str_Company = str_b + act.Tc.Company + str_e,
                    str_FIOcons = str_b + act.Tc.FIO + str_e,
                    str_ObjName = str_b + act.Tc.ObjName + str_e,
                    str_Address = str_b + act.Tc.Address + str_e,
                    str_Pow = str_b + act.Tc.Pow + str_e,
                    str_Category = str_b + act.Tc.Category.ToString() + str_e,
                    str_TP = str_b + act.Tc.TP + str_e,
                    str_Line04 = str_b + act.Tc.Line04 + str_e,
                    str_Pillar = str_b + act.Tc.Pillar + str_e,
                    str_InvNum = str_b + act.Tc.TP + str_e,
                    str_ConsBalance = str_b + act.ConsBalance + str_e,
                    str_DevBalance = str_b + act.DevBalance + str_e,
                    str_ConsExpl = str_b + act.ConsExpl + str_e,
                    str_DevExpl = str_b + act.DevExpl + str_e,
                    str_FIOtrans = str_b + act.FIOtrans + str_e,
                    str_Validity = str_b + act.Validity + str_e,
                    str_Nach = str_b + act.Tc.Res.Nach.Surname + " " + act.Tc.Res.Nach.Name.Substring(0, 1) + "." + act.Tc.Res.Nach.Patronymic.Substring(0, 1) + "." + str_e,
                    str_ZamNach = str_b + act.Tc.Res.ZamNach.Surname + " " + act.Tc.Res.ZamNach.Name.Substring(0, 1) + "." + act.Tc.Res.ZamNach.Patronymic.Substring(0, 1) + "." + str_e,
                    str_GlInzh = str_b + act.Tc.Res.GlInzh.Surname + " " + act.Tc.Res.GlInzh.Name.Substring(0, 1) + "." + act.Tc.Res.GlInzh.Patronymic.Substring(0, 1) + "." + str_e,
                    str_Buh = str_b + act.Tc.Res.Buh.Surname + " " + act.Tc.Res.Buh.Name.Substring(0, 1) + "." + act.Tc.Res.Buh.Patronymic.Substring(0, 1) + "." + str_e;
            string[] arrCons = str_FIOcons.Split(' ');
            string str_Cons = "";
            if (arrCons.Length >= 3)
            {
                str_Cons = str_b + arrCons[0] + " " + arrCons[1]?.Substring(0, 1) + "." + arrCons[2]?.Substring(0, 1) + "." + str_e;
                Petrovich FIOcons = new Petrovich() { AutoDetectGender = true, LastName = str_FIOcons.Split(' ')[0], FirstName = str_FIOcons.Split(' ')[1], MiddleName = str_FIOcons.Split(' ')[2] };
                var inflected = FIOcons.InflectTo(Case.Genitive);
                str_FIOcons = inflected.LastName + " " + inflected.FirstName + " " + inflected.MiddleName;
            }
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
            string contentRootPath = _webHostEnvironment.ContentRootPath;
            string webRootPath = _webHostEnvironment.WebRootPath;
            ViewBag.pathContent = contentRootPath;
            ViewBag.pathWeb = webRootPath;
            string path_docx = webRootPath + "\\Output\\docx\\" + str_Id + ".docx";
            string path_html = webRootPath + "\\Output\\html\\" + str_Id + ".html";
            string path_pdf = webRootPath + "\\Output\\pdf\\" + str_Id + ".pdf";
            ViewBag.path_docx = path_docx;
            var doc = new DocumentModel();
            var ActFont = new CharacterStyle("ActFont") { CharacterFormat = { Spacing = 1, Bold = true } };
            doc.Styles.Add(ActFont);
            doc.DefaultParagraphFormat.LineSpacing = 1;
            doc.DefaultCharacterFormat.FontName = "Times New Roman";
            doc.DefaultCharacterFormat.Size = 12;
            //---section1---//
            Section section1 = new Section(doc);
            doc.Sections.Add(section1);
            Paragraph paragraph = new Paragraph(doc) { ParagraphFormat = { Alignment = HorizontalAlignment.Center } };
            section1.Blocks.Add(paragraph);
            Run run1 = new Run(doc, "АКТ");
            Run run2 = new Run(doc, "разграничения балансовой принадлежности электросетей") { CharacterFormat = { Style = ActFont } };
            Run run3 = new Run(doc, "и эксплуатационной ответственности сторон") { CharacterFormat = { Style = ActFont } };
            paragraph.Inlines.Add(run1);
            paragraph.Inlines.Add(new SpecialCharacter(doc, SpecialCharacterType.LineBreak));
            paragraph.Inlines.Add(run2);
            paragraph.Inlines.Add(new SpecialCharacter(doc, SpecialCharacterType.LineBreak));
            paragraph.Inlines.Add(run3);
            //---table---//
            var table = new Table(doc) { TableFormat = { PreferredWidth = new TableWidth(100, TableWidthUnit.Percentage) } };
            table.TableFormat.Borders.SetBorders(MultipleBorderTypes.All, BorderStyle.None, Color.Black, 1);
            var row = new TableRow(doc);
            table.Rows.Add(row);
            var cell_left = new TableCell(doc, new Paragraph(doc, new Run(doc, "г. " + str_City)) { ParagraphFormat = { Alignment = HorizontalAlignment.Left } });
            var cell_right = new TableCell(doc, new Paragraph(doc, new Run(doc, str_DateAct)) { ParagraphFormat = { Alignment = HorizontalAlignment.Right } });
            row.Cells.Add(cell_left);
            row.Cells.Add(cell_right);
            section1.Blocks.Add(table);
            //---
            string str_act = "\nРУП «Брестэнерго» именуемое в дальнейшем «Энергоснабжающая организация», в лице начальника " + str_RESa + " РЭС филиала «Пинские электрические сети» РУП «Брестэнерго» "+str_FIOnachRod+" действующего на основании доверенности "+str_Dover+"г. с одной стороны, и "+str_Entity+" лицо "+str_Company+" именуемое в дальнейшем «Потребитель», в лице "+str_FIOcons+" " + str_ConsDover + " с другой стороны составили настоящий АКТ о нижеследующем.\t";
            section1.Blocks.Add(new Paragraph(doc, str_act) { ParagraphFormat = { Alignment = HorizontalAlignment.Justify } });
            //---
            string str_ty = "На день составления Акта технические условия "+str_NumTc+" от "+str_DateTc+" \n " +
                "на внешнее электроснабжение объекта";
            section1.Blocks.Add(new Paragraph(doc, str_ty) { ParagraphFormat = { Alignment = HorizontalAlignment.Center } });
            //---
            string str_building = str_ObjName +
                ", находящегося по адресу " + str_Address + " выполнены:\t";
            section1.Blocks.Add(new Paragraph(doc, str_building) { ParagraphFormat = { Alignment = HorizontalAlignment.Justify } });
            //---
            string str_par = "\tРазрешенная к использованию мощность "+str_Pow+" кВт.\t\n" +
                "\tЭлектроустановки потребителя относятся к "+str_Category+" категории " +
                "по надежности электроснабжения.\t\n" +
                "\tСхема внешнего электроснабжения относится к "+str_Category+" категории по надежности электроснабжения.\t\n" +
                "\tЭнергоснабжающая организация не несет ответственности перед Потребителем за перерывы в электроснабжении при несоответствии схемы электроснабжения категории электроприемников Потребителя и повреждении оборудования, не находящегося у нее на балансе.\t\n" +
                "\tВ соответствии с главой 3 Правил электроснабжения границы раздела устанавливаются следующими:\t\n";
            Paragraph paragraph2 = new Paragraph(doc, str_par) { ParagraphFormat = { Alignment = HorizontalAlignment.Justify } };
            section1.Blocks.Add(paragraph2);
            //---
            section1.Blocks.Add(new Paragraph(doc,
                new SpecialCharacter(doc, SpecialCharacterType.Tab),
                new Run(doc, "I. По балансовой принадлежности:") { CharacterFormat = { Style = ActFont } },
                new SpecialCharacter(doc, SpecialCharacterType.Tab),
                new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                new Run(doc,str_Line04 +" от " + str_TP + " оп. №"+str_Pillar+" на балансе "+str_RESa+" РЭС."),
                new SpecialCharacter(doc, SpecialCharacterType.Tab),
                new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                new Run(doc, str_ConsBalance + " от оп. №" + str_Pillar + " " + str_Line04 + " от " + str_TP + " и внутреннее эл. оборудование расположенное по адресу " + str_Address + " находится на балансе Потребителя"),
                new SpecialCharacter(doc, SpecialCharacterType.Tab),
                new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                new Run(doc, "Граница раздела между "+str_RESom+" РЭС и Потребителем "+str_DevBalance+ " №" + str_Pillar +" " + str_Line04 + " от " + str_TP),
                new SpecialCharacter(doc, SpecialCharacterType.Tab),
                new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                new SpecialCharacter(doc, SpecialCharacterType.Tab),
                new Run(doc, "II. По Эксплутационной ответственности:") { CharacterFormat = { Style = ActFont } },
                new SpecialCharacter(doc, SpecialCharacterType.Tab),
                new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                new Run(doc, str_Line04 + " от " + str_TP + " оп. №" + str_Pillar + " на балансе " + str_RESa + " РЭС."),
                new SpecialCharacter(doc, SpecialCharacterType.Tab),
                new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                new Run(doc, str_ConsExpl + " от оп. №" + str_Pillar + " " + str_Line04 + " от " + str_TP + " и внутреннее эл. оборудование расположенное по адресу " + str_Address + " находится на балансе Потребителя"),
                new SpecialCharacter(doc, SpecialCharacterType.Tab),
                new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                new Run(doc, "Граница раздела между " + str_RESom + " РЭС и Потребителем " + str_DevExpl + " №" + str_Pillar + " " + str_Line04 + " от " + str_TP),
                new SpecialCharacter(doc, SpecialCharacterType.Tab),
                new SpecialCharacter(doc, SpecialCharacterType.LineBreak)
                )
            { ParagraphFormat = { Alignment = HorizontalAlignment.Justify } });
            ///////////////////////////////////---sectiion2---
            if (type != "agreement")
            {
                Section section2 = new Section(doc);
                doc.Sections.Add(section2);
                ///---
                Paragraph paragraph21 = new Paragraph(doc) { ParagraphFormat = { Alignment = HorizontalAlignment.Center } };
                section2.Blocks.Add(paragraph21);
                paragraph21.Inlines.Add(new Run(doc, "Схема питания электроустановки:"));
                paragraph21.Inlines.Add(new SpecialCharacter(doc, SpecialCharacterType.LineBreak));
                // find picture to insert
                string path_pict = (_webHostEnvironment.WebRootPath + "\\Output\\png\\");
                DirectoryInfo dirPNG = new DirectoryInfo(path_pict);
                FileInfo pictFile = dirPNG.EnumerateFiles().Where(p => p.Name.Split('.')[0] == act.Id.ToString()).FirstOrDefault();
                if (pictFile != null)
                {
                    path_pict += pictFile.Name;
                    Picture pict = new Picture(doc, path_pict, 159, 106, LengthUnit.Millimeter);
                    paragraph21.Inlines.Add(pict);
                }
                //---
                section2.Blocks.Add(new Paragraph(doc,
                    new SpecialCharacter(doc, SpecialCharacterType.Tab),
                    new Run(doc, "ПРИМЕЧАНИЕ"),
                    new SpecialCharacter(doc, SpecialCharacterType.Tab),
                    new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                    new SpecialCharacter(doc, SpecialCharacterType.Tab),
                    new Run(doc, "1.Границы по схеме обозначаются: балансовой принадлежности - красной линией; эксплуатационной ответственности - синей."),
                    new SpecialCharacter(doc, SpecialCharacterType.Tab),
                    new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                    new SpecialCharacter(doc, SpecialCharacterType.Tab),
                    new Run(doc, "2.При изменении срока действия Акта, присоединенных мощностей, схемы внешнего электроснабжения, категории надежности электроснабжения, границ балансовой принадлежности и эксплуатационной ответственности Акт подлежит замене."),
                    new SpecialCharacter(doc, SpecialCharacterType.Tab),
                    new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                    new SpecialCharacter(doc, SpecialCharacterType.Tab),
                    new Run(doc, "3.Доверенность потребителя на подписание акта разграничения хранится в энергоснабжающей организации."),
                    new SpecialCharacter(doc, SpecialCharacterType.Tab),
                    new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                    new SpecialCharacter(doc, SpecialCharacterType.Tab),
                    new Run(doc, "4.На схеме питания электроустановки указываются места установки приборов учета, параметры силовых и измерительных трансформаторов и ЛЭП."),
                    new SpecialCharacter(doc, SpecialCharacterType.Tab),
                    new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                    new SpecialCharacter(doc, SpecialCharacterType.Tab),
                    new Run(doc, "5.Потребителю запрещается без согласования с диспетчером энергоснабжающей организации самовольно производить переключения и изменять схему внешнего электроснабжения."),
                    new SpecialCharacter(doc, SpecialCharacterType.Tab),
                    new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                    new SpecialCharacter(doc, SpecialCharacterType.Tab),
                    new Run(doc, "6.Потребителю запрещается без согласования с энергоснабжающей организацией подключать к своим электроустановкам сторонних потребителей."),
                    new SpecialCharacter(doc, SpecialCharacterType.Tab),
                    new SpecialCharacter(doc, SpecialCharacterType.LineBreak)
                    )
                { ParagraphFormat = { Alignment = HorizontalAlignment.Justify } }
                );
                //---
                var table2 = new Table(doc) { TableFormat = { PreferredWidth = new TableWidth(100, TableWidthUnit.Percentage) } };
                table2.TableFormat.Borders.SetBorders(MultipleBorderTypes.All, BorderStyle.None, Color.Black, 1);
                var row21 = new TableRow(doc);
                table2.Rows.Add(row21);
                var cell2_left = new TableCell(doc, new Paragraph(doc,
                    new Run(doc, "Представитель энергоснабжающей организации"),
                    new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                    new Run(doc, "Представитель Потребителя"),
                    new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                    new Run(doc, "Представитель владельца"),
                    new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                    new Run(doc, "транзитных электрических сетей"),
                    new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                    new Run(doc, "Срок действия акта"),
                    new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                    new Run(doc, "Главный инженер"),
                    new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                    new Run(doc, "Зам.начальника РЭС по сбыту энерги"),
                    new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                    new Run(doc, "Бухгалтер РЭС")
                    ));
                var cell2_center = new TableCell(doc, new Paragraph(doc,
                    new Run(doc, "_____"),
                    new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                    new Run(doc, "_____"),
                    new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                    new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                    new Run(doc, "_____"),
                    new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                    new Run(doc, str_Validity),
                    new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                    new Run(doc, "_____"),
                    new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                    new Run(doc, "_____"),
                    new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                    new Run(doc, "_____")
                    ));
                var cell2_right = new TableCell(doc, new Paragraph(doc,
                    new Run(doc, str_Nach),
                    new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                    new Run(doc, str_Cons),
                    new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                    new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                    new Run(doc, str_FIOtrans),
                    new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                    new Run(doc, ""),
                    new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                    new Run(doc, str_GlInzh),
                    new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                    new Run(doc, str_ZamNach),
                    new SpecialCharacter(doc, SpecialCharacterType.LineBreak),
                    new Run(doc, str_Buh)
                    ));
                row21.Cells.Add(cell2_left);
                row21.Cells.Add(cell2_center);
                row21.Cells.Add(cell2_right);
                section2.Blocks.Add(table2);
                //---save---//
                doc.Save(path_docx);
                doc.Save(path_pdf);
                _context.d_Acts.FirstOrDefault(p => p.Id == act.Id).State = (int)Stat.Completed;
            }
            else
            {
                doc.Save(path_html);
                _context.d_Acts.FirstOrDefault(p => p.Id == act.Id).State = (int)Stat.Agreement;
            }
            _context.SaveChanges();
        }
    }
}
