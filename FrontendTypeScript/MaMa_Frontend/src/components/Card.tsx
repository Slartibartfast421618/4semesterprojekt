import type { HairdresserView } from "../hooks/useHairdressersSearch"

type CardProps = {
    name: string;
    distance: number;
    availableTimeslots: number;
    price: number;
    website: string;
};

type CardData = {
    data: HairdresserView;
}

export default function Card({
    data,
}: CardData) {
    return (
        <div className="card">
            <div className="segment_info">
                <p>
                    {data.salonName}
                </p>
            </div>
            <div className="segment_info">
                <p>
                    {data.distance}km
                </p>
            </div>
            <div className="segment_info">
                <p>
                    {/*{data.availableTimeslots}*/}
                    {7777777} {/*Placeholder number*/}
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
                    {data.treatments}kr
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
