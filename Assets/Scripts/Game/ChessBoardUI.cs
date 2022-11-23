using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChessBoardUI : MonoBehaviour
{
    public Text debugText;
    private Transform ChessBoardPieces;
    public int pieceType = 0;
    private ChessBoardController chessBoardController;
    // Start is called before the first frame update
    void Awake()
    {
        chessBoardController = ChessBoardController.Instance;
        debugText = transform.parent.Find("DebugText").GetComponent<Text>();
        ChessBoardPieces = transform.Find("ChessBoardPieces");
    }
    private void OnEnable()
    {
        EventHandler.ShowScoreEvent += OnShowScoreEvent;
        EventHandler.GenerateChessEvent += OnGenerateChessEvent;
        EventHandler.GameOverEvent += OnGameOverEvent;
        EventHandler.UpdateDebugEvent += OnUpdateDebugEvent;
        EventHandler.NewStartGameEvent += OnNewStartGameEvent;
    }
    private void OnDisable()
    {
        EventHandler.ShowScoreEvent -= OnShowScoreEvent;
        EventHandler.GenerateChessEvent -= OnGenerateChessEvent;
        EventHandler.GameOverEvent -= OnGameOverEvent;
        EventHandler.UpdateDebugEvent -= OnUpdateDebugEvent;
        EventHandler.NewStartGameEvent -= OnNewStartGameEvent;
    }

	private void OnNewStartGameEvent()
	{
        pieceType = 0;
        for (int i = 0; i < ChessBoardPieces.childCount; i++)
        {
            Destroy(ChessBoardPieces.GetChild(i).gameObject);;
        }
        EventHandler.CallUpdateDebugEvent(chessBoardController.RamainingRound(pieceType));
    }

	private void OnUpdateDebugEvent(int index)
	{
        debugText.text ="游戏进行中，还剩"+index+"个回合";

    }

	private void OnGameOverEvent()
    {
        Debug.Log("游戏结束");
        ChessBoardController.Instance.CalculateScore();
    }
    private void OnGenerateChessEvent(GameObject go)
    {
        Instantiate(chessBoardController.piecesList[pieceType % 2], go.transform.position, Quaternion.identity, ChessBoardPieces);
        chessBoardController.UpdateChessPieceArrays(int.Parse(go.name.Split(',')[0]), int.Parse(go.name.Split(',')[1]), (pieceType % 2) + 1);
        pieceType++;
        chessBoardController.isRoundOver(pieceType);
        
    }
    private void OnShowScoreEvent(int index1,int index2)
	{
        debugText.text = chessBoardController.piecesList[0].name + index1 + "    "+chessBoardController.piecesList[1].name + index2;
    }

    
}
