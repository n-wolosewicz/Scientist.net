using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GitHub.Internals
{
    /// <summary>
    /// Declares all of the settings necessary in order
    /// to create a new <see cref="ExperimentInstance{T, TClean}"/>.
    /// </summary>
    /// <typeparam name="T">The result type for the experiment.</typeparam>
    /// <typeparam name="TClean">The cleaned type of the experiment.</typeparam>
    internal class ExperimentSettings<T, TClean>
    {
        public Func<Task> BeforeRun { get; set; }
        public Dictionary<string, OrderedBehavior<T>> Candidates { get; set; }
        public Func<T, TClean> Cleaner { get; set; }
        public Func<T, T, bool> Comparator { get; set; }
        public Dictionary<string, dynamic> Contexts { get; set; }
        public OrderedBehavior<T> Control { get; set; }
        public Func<Task<bool>> Enabled { get; set; }
        public IEnumerable<Func<T, T, Task<bool>>> Ignores { get; set; }
        public string Name { get; set; }
        public int ConcurrentTasks { get; set; }
        public Func<Task<bool>> RunIf { get; set; }
        public bool ThrowOnMismatches { get; set; }
        public Action<Operation, Exception> Thrown { get; set; }
        public IResultPublisher ResultPublisher { get; set; }
    }

    internal class OrderedBehavior<T>
    {
        public Func<Task<T>> Behavior { get; }
        public int? ExecutionOrder { get; }

        public OrderedBehavior(Func<Task<T>> behavior)
        {
            Behavior = behavior;
            ExecutionOrder = null;
        }

        public OrderedBehavior(Func<T> behavior) : this(() => Task.FromResult(behavior.Invoke()))
        {

        }

        public OrderedBehavior(Func<Task<T>> behavior, int executionOrder) : this(behavior)
        {
            ExecutionOrder = executionOrder;
        }

        public OrderedBehavior(Func<T> behavior, int executionOrder) : this(behavior)
        {
            ExecutionOrder = executionOrder;
        }
    }
}
