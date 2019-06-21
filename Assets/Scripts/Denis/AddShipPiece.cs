using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AddShipPiece : MonoBehaviour
{
    private int nbShipPieces;
    [SerializeField]
    private int requiredShipPieces;
    [SerializeField]
    private TextMeshProUGUI textMeshDisplayPieces;
    [SerializeField]
    private LeandreMapManager map;

    private bool full = false;

    void Start()
    {
        nbShipPieces = 0;
        updateCounterPieces();
    }

    void Update()
    {
    }

    #region Méthodes relatives aux pièces de vaisseau
    /// <summary> Ajouter une pièce de vaisseau </summary>
    public void AddPiece()
    {
        if (full) return;

        nbShipPieces++;
        updateCounterPieces();
        if (nbShipPieces >= requiredShipPieces)
        {
            full = true;
            map.FinishLevel();
        }
    }

    void updateCounterPieces()
    {
        textMeshDisplayPieces.text = string.Format("{0} / {1}", nbShipPieces, requiredShipPieces);
    }
    #endregion
}
