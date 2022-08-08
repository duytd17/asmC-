using Asm_c_sharp_3.Context;
using Asm_c_sharp_3.DomainClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asm_c_sharp_3.Repositories
{
    public class ChucVuRepository
    {
        private FpolyDBContext _dbContext;

        public ChucVuRepository()
        {
            _dbContext = new FpolyDBContext();
        }

        public bool AddChucVu(ChucVu cv)
        {
            if (cv == null) return false;
            cv.Id = Guid.NewGuid();
            _dbContext.Add(cv);
            _dbContext.SaveChanges();
            return true;
        }

        public bool UpdateChucVu(ChucVu cv)
        {
            if (cv == null) return false;
            var tempcv = _dbContext.ChucVus.FirstOrDefault(c => c.Id == cv.Id);
            tempcv.Ten = cv.Ten; 
            _dbContext.Update(tempcv);
            _dbContext.SaveChanges();
            return true;
        }

        public bool DeleteChucVu(ChucVu cv)
        {
            if (cv == null) return false;
            var tempcv = _dbContext.ChucVus.FirstOrDefault(c => c.Id == cv.Id);
            _dbContext.Remove(tempcv);
            _dbContext.SaveChanges();
            return true;
        }

        public List<ChucVu> GetAll()
        {
            return _dbContext.ChucVus.ToList();
        }
    }
}
