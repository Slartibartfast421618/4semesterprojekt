

type CardProps = {
    name: string;
    distance: int;
    availableTimeslots: int;
    price: number;
    website: string;
};

export default function Card({
    name,
    distance,
    availableTimeslots,
    price,
    website,
}: CardProps) {
    return (
        <div className="card">
            <div className="segment_info">
                <p>
                    {name}
                </p>
            </div>
            <div className="segment_info">
                <p>
                    {distance}km
                </p>
            </div>
            <div className="segment_info">
                <p>
                    {availableTimeslots}
                </p>
                <p>
                    ledige tider
                </p>
            </div>
            <div className="splitter" />
            <div className="segment_price">
                <p>
                    fra
                </p>
                <p className="price">
                    {price}kr
                </p>
                <button type="button" 
                    onClick={() => window.open(website, "_blank")}
                >
                    Se mere!
                </button>
            </div>
        </div>
    )
}
