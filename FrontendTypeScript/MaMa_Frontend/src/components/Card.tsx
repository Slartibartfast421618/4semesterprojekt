import type { HairdresserView } from "../hooks/useHairdressersSearch"
import getPrice from "../utility/getPrice";

type CardData = {
    fData: HairdresserView;
    cTreatment: string | null;
}

export default function Card({
    fData,
    cTreatment,
}: CardData) {
    const price = getPrice(fData, cTreatment);

    return (
        <div className="card">
            <div className="segment_info">
                <p>
                    {fData.salonName}
                </p>
            </div>
            <div className="segment_info">
                <p>
                    {fData.distance}km
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
                    {price !== null ? `${price}kr` : "-"}
                </p>
                <button type="button" 
                    onClick={() => window.open(fData.website ?? "", "_blank")}
                >
                    Se mere!
                </button>
            </div>
        </div>
    )
}
