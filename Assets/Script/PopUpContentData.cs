using UnityEngine;
using System.Collections.Generic; // Untuk List<string>

// Ini akan memungkinkan Anda membuat aset PopupContentData di Unity Editor
[CreateAssetMenu(fileName = "PopupContentData", menuName = "Data/Popup Content Data")]
public class PopupContentData : ScriptableObject
{
   
    public string popupTitle;
    [TextArea(5, 10)]
    public List<string> pagesOfTextIsi;

    
  
}