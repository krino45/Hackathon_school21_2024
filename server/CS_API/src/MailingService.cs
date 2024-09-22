using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MyApi.Models;
using MyApi.Services;

namespace MyApi
{
  public class MailingService
  {
    private class EventQueue
    {
      public TimeSpan TimeBeforeEvent { get; set; }
      public Queue<BaseEvent> Events { get; set; } = new Queue<BaseEvent>();
    }
    EventService? eventService;
    // Очереди для различных уведомлений
    private EventQueue queue24Hours = new EventQueue { TimeBeforeEvent = TimeSpan.FromHours(24) };
    private EventQueue queue12Hours = new EventQueue { TimeBeforeEvent = TimeSpan.FromHours(12) };
    private EventQueue queue2Hours = new EventQueue { TimeBeforeEvent = TimeSpan.FromHours(2) };

    private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

    public void Start()
    {
      Task.Run(() => ProcessEvents(_cancellationTokenSource.Token));
    }

    public void Stop()
    {
      _cancellationTokenSource.Cancel();
    }

    // Метод для обработки очередей
    // По задумке должен запускаться утром, получать все события следующего дня, распределять их по очередям
    // и в зависмости от очереди заранее отправлять сообщения
    private async Task ProcessEvents(CancellationToken cancellationToken)
    {
      while (!cancellationToken.IsCancellationRequested)
      {
        // var jsonResult = await eventService.GetAllEventsAsyncJSON();
        // List<BaseEvent> events = new List<BaseEvent>();
        BaseEvent test_event1 = new PrivateEvent();
        test_event1.StartTime = DateTime.Now.AddHours(14).AddMinutes(13);
        test_event1.AttendeesId = ["pavlova.v.pav@yandex.ru"];
        BaseEvent test_event2 = new RoundtableEvent();
        test_event2.StartTime = DateTime.Now.AddHours(2).AddMinutes(14);
        test_event2.AttendeesId = ["noobiktoobik@gmail.com"];
        List<BaseEvent> events = [test_event1, test_event2];
        foreach (BaseEvent eventItem in events)
        {
          TimeSpan timeToStart = (TimeSpan)(eventItem.StartTime - DateTime.Now);

          if (timeToStart.TotalHours >= 24)
          {
            queue24Hours.Events.Enqueue(eventItem);
          }
          else if (timeToStart.TotalHours >= 12)
          {
            queue12Hours.Events.Enqueue(eventItem);
          } else
          {
            queue2Hours.Events.Enqueue(eventItem);
          }
        }
        // Ожидаем до 6 утра следующего дня
        // DateTime nextMorning = DateTime.Now.Date.AddDays(1).AddHours(6);
        ProcessQueue(queue24Hours);
        ProcessQueue(queue12Hours);
        ProcessQueue(queue2Hours);

        await Task.Delay(TimeSpan.FromSeconds(30), cancellationToken); // Пауза на 60 минут
      }
    }


    // Метод для обработки одной очереди
    private async void ProcessQueue(EventQueue queue)
    {
      while (queue.Events.Count > 0)
      {
        var currentEvent = queue.Events.Peek();
        List<string>? RecepientEmail = currentEvent.AttendeesId;
        // Проверяем, сколько осталось до события
        if ((queue.TimeBeforeEvent - TimeSpan.FromHours(2)) < TimeSpan.FromHours(3))
        {
          string timeToEvent = (currentEvent.StartTime - DateTime.Now).ToString().Split(".")[0];
          await SendEmail("До начала мероприятия осталось: " + timeToEvent, RecepientEmail);
          // Удаляем событие из очереди
          queue.Events.Dequeue();
        }
        else
        {
          break; // Переходим к следующей очереди, если время еще не подошло
        }
      }
    }
    public async Task SendEmail(string HtmlBody, List<string>? RecipientEmail)
    {
      // достаем значение из конфигурационного файла
      var config = new ConfigurationBuilder()
      .AddJsonFile("MailingConfig.json", optional: true, reloadOnChange: true)
      .Build();

      string? SmtpServer = config.GetValue<string>("Mailing:SmtpServer");
      int SmtpPort = config.GetValue<int>("Mailing:SmtpPort");
      string? senderEmail = config.GetValue<string>("Mailing:SenderEmail");
      string? mailingLogin = config.GetValue<string>("Mailing:MailingLogin");
      string? mailingPassword = config.GetValue<string>("Mailing:MailingPassword");
      string? subject = config.GetValue<string>("Mailing:Subject");
      // создаем письмо
      MailMessage mails = new MailMessage();
      mails.From = new MailAddress(senderEmail); // мы отправляем
      foreach (string recepient in RecipientEmail) // наши получатели
      {
        if (recepient != "" || recepient != null)
        {
          mails.To.Add(new MailAddress(recepient));
        }

      }
      mails.Subject = subject; // тема
      mails.Body = HtmlBody; // содержимое письма
      // создаем подключение
      using (var smtp = new SmtpClient(SmtpServer, SmtpPort))
      {
        smtp.EnableSsl = true;
        smtp.Credentials = new NetworkCredential(mailingLogin, mailingPassword);
        Console.WriteLine("Mail send to: " + RecipientEmail[0]);
        await smtp.SendMailAsync(mails);
      }
    }
  }
}



/*
  public async Task ScheduleEventEmailsAsync()
  {
    // Рассчитываем время отправки
    var dayBeforeTime = TimeEvent.AddMinutes(+1); // 8 вечера за день до мероприятия
    var morningTime = TimeEvent.AddMinutes(+2); // 10 утра в день мероприятия
    var twoHoursBeforeTime = TimeEvent.AddMinutes(+5); // 2 часа до мероприятия

    // Планируем отправку писем
    await ScheduleEmailAsync(dayBeforeTime, "Завтра состоится мероприятие!");
    await ScheduleEmailAsync(morningTime, "Напоминание о мероприятии сегодня!");
    await ScheduleEmailAsync(twoHoursBeforeTime, "Мероприятие начнется через два часа!");

  }

  private async Task ScheduleEmailAsync(DateTime sendTime, string body)
  {
    // Вычисляем время ожидания
    var timeSpan = sendTime - DateTime.Now;
    if (timeSpan <= TimeSpan.Zero)
    {
      Console.WriteLine($"Время отправки {sendTime} уже прошло.");
      return;
    }
    var timer = new Timer(async state =>
        {
          // Вызываем метод отправки
          await SendEmailAsync(body);
        }, null, timeSpan, Timeout.InfiniteTimeSpan);
  }
*/