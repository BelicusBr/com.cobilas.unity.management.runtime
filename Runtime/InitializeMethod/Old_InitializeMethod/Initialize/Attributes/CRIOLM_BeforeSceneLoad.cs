﻿using System;

namespace Cobilas.Unity.Management.Runtime {
    [Obsolete]
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class CRIOLM_BeforeSceneLoadAttribute : CRIOLMBaseAttribute {

        public CRIOLM_BeforeSceneLoadAttribute(int priority) : base(priority, CRIOLMType.BeforeSceneLoad) { }

        public CRIOLM_BeforeSceneLoadAttribute(CRIOLMPriority priorityType) : base(priorityType, CRIOLMType.BeforeSceneLoad) { }

        public CRIOLM_BeforeSceneLoadAttribute() : base(CRIOLMType.BeforeSceneLoad) { }
    }
}
