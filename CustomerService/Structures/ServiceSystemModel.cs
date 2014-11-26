using System.Collections.Generic;
using System.Linq;

namespace CustomerService.Structures
{

    public class ServiceSystemModel
    {
        private readonly MainWindow _gui;
        private ServiceResourse _zdrojCekajiciNaPrideleni;
        private readonly CustomersGenerator _generator = new CustomersGenerator();

        private static void ServiceResourcesInitialization(ICollection<ServiceResourse> serviceResource,
            uint serviceResoursesCount, ServiceResourse.EServiceResourseType type)
        {
            for (var index = 0; index < serviceResoursesCount; index++)
            {
                serviceResource.Add(new ServiceResourse(type));
            }
        }

        public ServiceSystemModel(MainWindow gui, uint serviceResoursesACount, uint serviceResoursesBCount)
        {
            _gui = gui;
            _zdrojCekajiciNaPrideleni = null;
            ServiceResourcesInitialization(_gui.ResourcesA, serviceResoursesACount, ServiceResourse.EServiceResourseType.A);
            ServiceResourcesInitialization(_gui.ResourcesB, serviceResoursesBCount, ServiceResourse.EServiceResourseType.B);
        }

        public Customer VygenerujZakaznika()
        {
            return _generator.Generate();
        }

        public void PremisteniZakaznikaNaObsluhu(Customer customer)
        {
            _gui.IncomingCustomers.Add(customer);
        }

        public void PremistitZakaznikaDoFrontyNaObsluhu(Customer customer)
        {
            if (_gui.IncomingCustomers.Contains(customer))
            {
                _gui.IncomingCustomers.Remove(customer);
                _gui.CustomersWaitingOnServiceA.Add(customer);
            }
            else
            {
                _gui.MovingCustomersToServiceB.Remove(customer);
                _gui.CustomersWaitingOnServiceB.Add(customer);
            }
        }

        public void ZacniObsluhu(Customer customer, ServiceResourse resource)
        {
            if (resource.Type == ServiceResourse.EServiceResourseType.A)
            {
                _gui.IncomingCustomers.Remove(customer);
                _gui.CustomersWaitingOnServiceA.Remove(customer);
                _gui.CustomersInServiceA.Add(customer);
            }
            else
            {
                _gui.MovingCustomersToServiceB.Remove(customer);
                _gui.CustomersWaitingOnServiceB.Remove(customer);
                _gui.CustomersInServiceB.Add(customer);
            }
        }

        public void DokonciObsluhu(Customer customer, ServiceResourse resource)
        {
            if (resource.Type == ServiceResourse.EServiceResourseType.A)
            {
                _gui.CustomersInServiceA.Remove(customer);
                _gui.MovingCustomersToServiceB.Add(customer);
            }
            else
            {
                _gui.CustomersInServiceB.Remove(customer);
                _gui.FinishedCustomers.Add(customer);
            }
        }

        public void PremisteniZakaznikaZObsluhy(Customer customer)
        {
            _gui.FinishedCustomers.Remove(customer);
            _gui.OutgoingCustomers.Add(customer);
        }

        public ServiceResourse PridelZdrojZakaznikovi(Customer customer)
        {
            if (_zdrojCekajiciNaPrideleni.Type == ServiceResourse.EServiceResourseType.A)
            {
                _gui.ResourcesA.Remove(_zdrojCekajiciNaPrideleni);
            }
            else
            {
                _gui.ResourcesB.Remove(_zdrojCekajiciNaPrideleni);
            }
            return _zdrojCekajiciNaPrideleni;
        }

        public bool JePotrebaPremistPridelenyZdroj(ServiceResourse resourse)
        {
            return resourse.Type == ServiceResourse.EServiceResourseType.B;
        }

        public ServiceResourse VyberVolnehoZdroje(Customer customer)
        {
            //kdyz zadatel ceka na zdroj A
            if(_gui.IncomingCustomers.Contains(customer))
            {
                if (!_gui.ResourcesA.Any())
                {
                    _zdrojCekajiciNaPrideleni = null;
                    return null;
                }
                _zdrojCekajiciNaPrideleni = _gui.ResourcesA[0];
            } //kdyz zadatel ceka na zdroj B
            else
            {
                if (!_gui.ResourcesB.Any())
                {
                    _zdrojCekajiciNaPrideleni = null;
                    return null;
                }
                _zdrojCekajiciNaPrideleni = _gui.ResourcesB[0];
            }
            return _zdrojCekajiciNaPrideleni;
        }

        public void UvolneniZdroje(ServiceResourse resource)
        {
            if (resource.Type == ServiceResourse.EServiceResourseType.A)
            {
                _gui.ResourcesA.Add(resource);
            }
            else
            {
                _gui.ResourcesB.Add(resource);
            }
            _zdrojCekajiciNaPrideleni = resource;
        }

        public Customer VratZakaznikaCekajicihoVeFronte(ServiceResourse.EServiceResourseType typeResource)
        {
            Customer customer = null;
            if(typeResource == ServiceResourse.EServiceResourseType.A)
            //if (_gui.CustomersWaitingOnServiceA.Any())
            {
                customer = _gui.CustomersWaitingOnServiceA[0];
                _gui.CustomersWaitingOnServiceA.RemoveAt(0);
            }
            if (typeResource == ServiceResourse.EServiceResourseType.B)
            {
                customer = _gui.CustomersWaitingOnServiceB[0];
                _gui.CustomersWaitingOnServiceB.RemoveAt(0);
            }

            return customer;
        }

        public bool JeFrontaNaObsluhuPrazdna(ServiceResourse resource)
        {
            return (resource.Type == ServiceResourse.EServiceResourseType.A) ?
                !_gui.CustomersWaitingOnServiceA.Any() : !_gui.CustomersWaitingOnServiceB.Any();
        }
    }
}






