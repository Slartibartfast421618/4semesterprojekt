import { useState, useEffect } from "react";
import { useRequestGate } from "./utility/Debounce";
import useHairdresserSearch from "./hooks/useHairdressersSearch";
import useFilteredHairdressers from "./hooks/useFilteredHairddressers";
import TreatmentDropdown from "./components/TreatmentDropdown";
import Navbar from "./components/Navbar";
import Card from "./components/Card";
import fakeMap from './assets/fake_maps.png';
import './App.css';

function App() {
    // Navbar input
    const [place, setPlace] = useState("");
    const [start, setStart] = useState("");
    const [end, setEnd] = useState("");
    // Input trigger
    const gatedPlace = useRequestGate(place);
    // API handling
    const { data, allTreatments, loading, error } = useHairdresserSearch(gatedPlace);
    // Dropdown filtering
    const [chosenTreatment, setChosenTreatment] = useState("");
    useEffect(() => { setChosenTreatment(""); }, [data]);
    const filteredData = useFilteredHairdressers(data, chosenTreatment) ?? [];

    return (
        <>
            <Navbar
                place={place}
                dateEarliest={start}
                dateLatest={end}
                onPlaceChange={setPlace}
                onDateEarliestChange={setStart}
                onDateLatestChange={setEnd}
            />
            <div className="split-container">
                <div className="split left">
                    <TreatmentDropdown
                        treatments={allTreatments}
                        selected={chosenTreatment}
                        onChange={setChosenTreatment}
                    />
                    {loading && <p>Loading results…</p>}
                    {error && <p style={{ color: "red" }}>{error}</p>}
                    {!loading && !error && data && data.length > 0 ? (
                        filteredData.map((h, index) => (
                            <Card
                                key={index}
                                fData={h}
                                cTreatment={chosenTreatment}
                            />
                        ))
                    ) : (
                        !loading && !error && <p>No results found.</p>
                    )}
                </div>
                <div className="split right">
                    <div className="test up">
                        <h2>
                            Nearby locations
                        </h2>
                    </div>
                    <div className="test down">
                        <img src={fakeMap} className="fakemap" alt="This isn't a real, interactive map." />
                    </div>
                </div>
            </div>
        </>
    )
}

export default App
