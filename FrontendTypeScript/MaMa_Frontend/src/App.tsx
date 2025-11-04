import { useState, useEffect } from "react";
import { useRequestGate } from "./utility/Debounce";
import { useHairdresserSearch } from "./components/useHairdressersSearch";
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
    const { data, loading, error } = useHairdresserSearch(gatedPlace);

    return (
        <>
            <Navbar
                place={place}
                start={start}
                end={end}
                onPlaceChange={setPlace}
                onStartChange={setStart}
                onEndChange={setEnd}
            />
            <div className="split-container">
                <div className="split left">
                  <p>Navn - Distance - Ledige tider - Priser fra</p>
                    {loading && <p>Loading results…</p>}
                    {error && <p style={{ color: "red" }}>{error}</p>}

                    {!loading && !error && data && data.length > 0 ? (
                        data.map((h, index) => (
                            <Card
                                key={index}
                                name={h.salonName ?? "Ukjent salong"}
                                distance={h.distance ?? 0}
                                availableTimeslots={0} // placeholder for now
                                price={h.price ?? 0}
                                website={h.website ?? ""}
                            />
                        ))
                    ) : (
                        !loading && !error && <p>No results found.</p>
                    )}
                  <Card
                      name={"KlipKlapperne"}
                      distance={5}
                      availableTimeslots={12}
                      price={1324}
                      website={"https://www.google.com/"}
                  />
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
