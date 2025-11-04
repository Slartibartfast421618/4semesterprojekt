import { useState } from "react";
import Navbar from "./components/Navbar";
import Card from "./components/Card";
import fakeMap from './assets/fake_maps.png';
import './App.css';

function App() {

    const [place, setPlace] = useState("");
    const [start, setStart] = useState("");
    const [end, setEnd] = useState("");

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
                  <Card
                      name={"Kalle's Klippere"}
                      distance={3}
                      availableTimeslots={13}
                      price={99}
                      website={"https://www.google.com/"}
                  />
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
