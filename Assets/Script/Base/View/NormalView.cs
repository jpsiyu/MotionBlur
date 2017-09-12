using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalView : IViewBase {
    #region interface
    public void Close() {
    }

    public void Open() {
    }

    public EViewType ViewType() {
        return EViewType.Normal;
    }
    #endregion 
}
