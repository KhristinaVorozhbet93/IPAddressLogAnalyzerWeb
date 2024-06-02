namespace IPAddressLogAnalyzer.WebAPI
{
    public class MyBackgroundService : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //1. скачали все файлы c Minio, но настройки поменять здесь, они будут браться с другого места
            //2. считали с них логи с помозью сервиса
            //3. Записали все в бд
            //настройки клиента передавались извне
            //нужно чтобы была возможность завершения
            //чтобы не обрабатывать уже обрабтанные записи и файлы
            //обработка происходила по истечении какого-то времени
        }
    }
}
