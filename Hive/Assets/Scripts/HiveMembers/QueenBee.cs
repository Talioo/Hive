public class QueenBee : HiveMember
{
    protected override void Start()
    {
        base.Start();
        SOInstances.SOHexaInfo.OnAddCell += CheckGameOver;
    }

    private void CheckGameOver(HexaCell hexaCell)
    {
        if (myCell.neighbours.Count == Constants.CellsPerHexaCell)
            SOInstances.GameManager.Lose();
    }
    private void OnDisable()
    {
        SOInstances.SOHexaInfo.OnAddCell -= CheckGameOver;
    }
}
