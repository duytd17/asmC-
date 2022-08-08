using Asm_c_sharp_3.Context;
using Asm_c_sharp_3.DomainClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asm_c_sharp_3.Repositories
{
    public class CuaHangRepository
    {
        private FpolyDBContext _dbContext;
        private List<CuaHang> _lstCuaHang;

        public CuaHangRepository()
        {
            _dbContext = new FpolyDBContext();
            _lstCuaHang = new List<CuaHang>();
        }
        public List<CuaHang> GetListCuaHangDB()
        {
            return _lstCuaHang = _dbContext.CuaHangs.ToList();
        }
        public bool AddCuaHang(CuaHang ch)
        {
            if(ch == null)  return false;
            ch.Id = Guid.NewGuid();
            _dbContext.CuaHangs.Add(ch);
            _dbContext.SaveChanges();
            return true;
        }
        public bool UpdateCuaHang(CuaHang ch)
        {
            if (ch == null) return false;
            var tempch = _dbContext.CuaHangs.FirstOrDefault(c => c.Id == ch.Id);
            tempch.Ten = ch.Ten;
            tempch.Ma = ch.Ma;
            tempch.DiaChi= ch.DiaChi;
            tempch.QuocGia = ch.QuocGia;
            tempch.ThanhPho = ch.ThanhPho;
            _dbContext.Update(tempch);
            _dbContext.SaveChanges();
            return true;
        }
        public bool DeleteCuaHang(CuaHang ch)
        {
            if (ch == null) return false;
            var tempch = _dbContext.CuaHangs.FirstOrDefault(c => c.Id == ch.Id);
            _dbContext.Remove(tempch);
            _dbContext.SaveChanges();
            return true;
        }
        public List<CuaHang> GetAll()
        {
            return _dbContext.CuaHangs.ToList();
        }
    }
}
