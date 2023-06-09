﻿using Flunt.Notifications;

namespace GestaoCurso.Domain.ViewModels
{
    public class ResultViewModel<T>
    {
        public ResultViewModel(T data, List<string> errors)
        {
            Data = data;
            Errors = errors;
        }

        public ResultViewModel(T data)
        {
            Data = data;
        }

        public ResultViewModel(List<string> errors)
        {
            Errors = errors;
        }

        public ResultViewModel(string error)
        {
            Errors.Add(error);
        }

        public ResultViewModel(List<Notification> notifications)
        {
            Errors = notifications.Select(x => x.Message).ToList();
        }

        public T Data { get; private set; }
        public List<string> Errors { get; private set; } = new();
    }
}
