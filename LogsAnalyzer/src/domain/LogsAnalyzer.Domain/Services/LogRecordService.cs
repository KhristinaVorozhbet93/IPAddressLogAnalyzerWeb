using LogsAnalyzer.Domain.Entities;
using LogsAnalyzer.Domain.Exceptions;
using LogsAnalyzer.Domain.Interfaces;

namespace LogsAnalyzer.Domain.Services
{
    public class LogRecordService
    {
        private readonly ILogRepository _logRepository;

        public LogRecordService (ILogRepository logRepository)
        {
            _logRepository = logRepository
                ?? throw new ArgumentNullException(nameof(logRepository));
        }

        public virtual async Task<LogRecord> GetLogRecord(Guid id, CancellationToken cancellationToken)
        {
            var logRecord = await _logRepository.GetById(id, cancellationToken);

            if (logRecord is null)
            {               
                throw new LogRecordNotFoundException(nameof(logRecord));
            }
            return logRecord;
        }

        public virtual async Task<List<LogRecord>> GetLogRecordss(CancellationToken cancellationToken)
        {
            var logRecords = await _logRepository.GetAll(cancellationToken);

            if (logRecords is null)
            {
                throw new ArgumentNullException(nameof(logRecords));
            }
            return logRecords;
        }

        public virtual async Task AddLogRecord(LogRecord logRecord, CancellationToken cancellationToken)
        {
        //    ArgumentException.ThrowIfNullOrWhiteSpace(nameof(applicationName));
        //    ArgumentException.ThrowIfNullOrWhiteSpace(nameof(stage));
        //    ArgumentException.ThrowIfNullOrWhiteSpace(nameof(clientName));
        //    ArgumentException.ThrowIfNullOrWhiteSpace(nameof(clientVersion));
        //    ArgumentException.ThrowIfNullOrWhiteSpace(nameof(path));
        //    ArgumentException.ThrowIfNullOrWhiteSpace(nameof(method));
        //    ArgumentException.ThrowIfNullOrWhiteSpace(nameof(statusCode));
        //    ArgumentException.ThrowIfNullOrWhiteSpace(nameof(statusMessage));
        //    ArgumentException.ThrowIfNullOrWhiteSpace(nameof(contentType));

        //    if (contentLength < 0)
        //    {
        //        throw new ArgumentNullException(nameof(contentLength));
        //    }
        //    if (memoryUsage < 0)
        //    {
        //        throw new ArgumentNullException(nameof(memoryUsage));
        //    }

        //    var logRecord = new LogRecord(Guid.NewGuid(), requestTime, applicationName, stage, сlientIpAddress, clientName, clientVersion, path, method,
        //                    statusCode, statusMessage, contentType, contentLength, executionTime, memoryUsage);

            await _logRepository.Add(logRecord, cancellationToken);
        }
      
        public virtual async Task DeleteLogRecord(Guid id, CancellationToken cancellationToken)
        {
            var logRecord = await _logRepository.GetById(id, cancellationToken);
            if (logRecord is null)
            {
                throw new LogRecordNotFoundException(nameof(logRecord));
            }
            await _logRepository.Delete(logRecord, cancellationToken);
        }
    }
}

