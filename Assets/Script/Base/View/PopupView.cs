using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupView : IViewBase {
    #region interface
    public void Close() {
    }

    public void Open() {
    }

    public EViewType ViewType() {
        return EViewType.Popup;
    }
    #endregion
}
