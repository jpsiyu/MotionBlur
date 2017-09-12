using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IViewBase {
    void Open();
    void Close();
    EViewType ViewType();
}
