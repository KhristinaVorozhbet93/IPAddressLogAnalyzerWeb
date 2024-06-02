using System.Net;

namespace IPAddressLogAnalyzer.Domain.Entities
{
    public class AccesLog : IComparable<AccesLog>
    {
        private Guid _id;
        private IPAddress _address;
        private DateTime _timeRequest;
        private int _requstCount;
        private string _resource;
        private string _path;
        private string _method;
        private string _response;

        public AccesLog(IPAddress address, 
            DateTime timeRequest,
            int requestCount,
            string resource,
            string path,
            string method,
            string response)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(address));
            _address = address;
            _timeRequest = timeRequest;
            _requstCount = requestCount;
            _resource= resource;
            _path= path;
            _method= method;
            _response= response;    
        }

        public Guid Id
        {
            get
            {
                return _id;
            }
            init
            {
                _id = value;
            }
        }

        public IPAddress Address
        {
            get
            {
                return _address;
            }
            init
            {
                ArgumentException.ThrowIfNullOrEmpty(nameof(value));
                _address = value;
            }
        }

        public DateTime TimeRequest
        {
            get
            {
                return _timeRequest;
            }
            set
            {
                _timeRequest = value;
            }
        }

        public int RequestCount
        {
            get
            {
                return _requstCount;
            }
            set
            {
                if (value <= 0) throw new IndexOutOfRangeException(nameof(value)); 
                _requstCount = value;
            }
        }

        public string Resource
        {
            get
            {
                return _resource;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(value));
                }
                _resource = value;
            }
        }

        public string Path
        {
            get
            {
                return _path;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(value));
                }
                _path = value;
            }
        }

        public string Method
        {
            get
            {
                return _method;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(value));
                }
                _method = value;
            }
        }

        public string Response
        {
            get
            {
                return _response;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(value));
                }
                _response = value;
            }
        }
        public int CompareTo(AccesLog? other)
        {
            if (other is null)
            {
                return 1; 
            }

            int addressComparison = Address.ToString().CompareTo(other.Address.ToString());
            if (addressComparison != 0)
            {
                return addressComparison;
            }
            return TimeRequest.CompareTo(other.TimeRequest);
        }

        public override string ToString()
        {
            return $"{Address} {TimeRequest} {RequestCount} {Resource} {Path} {Method} {Response}";
        }
    }
}



