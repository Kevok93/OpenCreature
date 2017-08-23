public struct LevelMove {
    public byte level;
    public int move_id;
    public Move move;
}
public struct Evolution {
    public int value; //can be level, happiness, or item id
    public int species_id;
    public Species species;
    public EvolutionType type;
    //public Item item;
}