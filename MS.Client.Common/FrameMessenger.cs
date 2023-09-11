using Prism.Events;
using Prism.Ioc;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MS.Client.Common
{
    public static class FrameMessenger
    {
        /// <summary>
        /// 发布方法
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="t">T类型消息</param>
        public static void Publish<T>(this IEventAggregator _eventAggregator, T t)
        {
            _eventAggregator.GetEvent<PubSubEvent<T>>().Publish(t);
        }

        /// <summary>
        /// 订阅T类型消息
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="action">订阅方法，用于执行接受消息的动作</param>
        public static void Subscribe<T>(this IEventAggregator _eventAggregator, Action<T> action)
        {
            _eventAggregator.GetEvent<PubSubEvent<T>>().Subscribe(action);
        }

        /// <summary>
        /// 订阅T类型消息，且增加过滤条件
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="action">订阅方法，用于执行接受消息的动作</param>
        /// <param name="filter">过滤条件</param>
        public static void Subscribe<T>(this IEventAggregator _eventAggregator, Action<T> action, Predicate<T> filter)
        {
            _eventAggregator.GetEvent<PubSubEvent<T>>().Subscribe(action, filter);
        }

        /// <summary>
        /// 订阅T类型消息，且增加过滤条件，是否同步执行，保持强引用
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="action">订阅方法，用于执行接受消息的动作</param>
        /// <param name="filter">过滤条件</param>
        /// <param name="sync">是否同步</param>
        /// <param name="keepSubscriberReferenceAlive">是否保持强引用</param>
        public static void Subscribe<T>(this IEventAggregator _eventAggregator,Action<T> action, Predicate<T> filter = null, bool sync = false, bool keepSubscriberReferenceAlive = false)
        {
            _eventAggregator.GetEvent<PubSubEvent<T>>().Subscribe(action,
                sync ? ThreadOption.PublisherThread : ThreadOption.BackgroundThread,
                keepSubscriberReferenceAlive,
                filter);
        }

        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="action">订阅方法</param>
        public static void Unsubscribe<T>(this IEventAggregator _eventAggregator,Action<T> action)
        {
            _eventAggregator.GetEvent<PubSubEvent<T>>().Unsubscribe(action);
        }
    }
}
