using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;

namespace Assignment4
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            // AutoMapper create map statements - using AutoMapper static API
            // Mapper.Initialize(cfg => cfg.CreateMap< FROM , TO >());
            // Add map creation statements here
            Mapper.Initialize(cfg => { cfg.CreateMap<Models.Invoice, Controllers.InvoiceBase>();
                                       cfg.CreateMap<Models.Invoice, Controllers.InvoiceWithDetail>();
                                       cfg.CreateMap<Models.InvoiceLine, Controllers.InvoiceLineBase>();
                                       cfg.CreateMap<Models.InvoiceLine, Controllers.InvoiceLineWithDetail>();
            });
        }
    }
}