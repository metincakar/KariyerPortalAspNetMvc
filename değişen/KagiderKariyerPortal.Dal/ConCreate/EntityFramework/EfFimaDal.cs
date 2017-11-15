using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Text;
using KagiderKariyerPortal.Dal.Abstract;
using KagiderKariyerPortal.Entities.ConCreate;

namespace KagiderKariyerPortal.Dal.ConCreate.EntityFramework
{
    public class EfFimaDal:IEntityDal<Firma>
    {
        KagiderContext _context=new KagiderContext();
        public void Add(Firma entity)
        {
            _context.Firmas.Add(entity);
            _context.SaveChanges();
        }

        public Firma Get(Firma entity)
        {
            return _context.Firmas.FirstOrDefault(f => f.FirmaId == entity.FirmaId);
        }

        public List<Firma> GetList()
        {
            return _context.Firmas.ToList();
        }

        public void Del(Firma entity)
        {
            _context.Firmas.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(Firma entity)
        {
          var  updFirma = _context.Firmas.FirstOrDefault(f => f.FirmaId == entity.FirmaId);
            if (updFirma != null)
            {
                updFirma.Adres = entity.Adres;
                updFirma.CalisanSayisi = entity.CalisanSayisi;
                updFirma.Email = entity.Email;
                updFirma.FaliyetGosterdigiIller = entity.FaliyetGosterdigiIller;
                updFirma.FirmaAd = entity.FirmaAd;
                updFirma.FirmaCalismaAlanlari = entity.FirmaCalismaAlanlari;
                updFirma.FirmaHakkinda = entity.FirmaHakkinda;
                updFirma.SorumluAd = entity.SorumluAd;
                updFirma.SorumluPozisyon = entity.SorumluPozisyon;
                updFirma.SorumluSoyAd = entity.SorumluSoyAd;
                updFirma.Tel = entity.Tel;
                updFirma.UserId = entity.UserId;
            }

            _context.SaveChanges();
        }
    }
}
