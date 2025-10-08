import mamaLogo from './assets/manemanager_nbg_close.png'
import fakeMap from './assets/fake_maps.png'
import './App.css'

function App() {

  return (
      <>
          <div className="navbar">
                <a href="http://localhost:7000" target="_blank">
                    <img src={mamaLogo} className="logo" alt="Mane Manager logo" />
                </a>
                <div className="stack textbox">
                    <span className="name">Mane Manager</span>
                    <span className="slogan">Mane management made mild</span>
                </div>
              <div className="spacer" />
              <div className="stack">
                  <span className="searchterm">Place</span>
                  <input className="searchinput" />
              </div>
              <div className="stack">
                  <span className="searchterm">TimeDate start</span>
                  <input className="searchinput" />
              </div>
              <div className="stack">
                  <span className="searchterm">TimeDate end</span>
                  <input className="searchinput" />
              </div>
                <div className="spacer" />
          </div>
          <div className="split-container">
              <div className="split left">
                <p>Navn - Distance - Ledige tider - Priser fra</p>

                  <div className="card">
                      <div className="segment_info">
                          <p>
                              Kalle's Korte Klip
                          </p>
                      </div>
                      <div className="segment_info">
                          <p>
                              3km
                          </p>
                      </div>
                      <div className="segment_info">
                          <p>
                            3
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
                              99 kr
                          </p>
                          <button>
                              Se mere
                          </button>
                      </div>
                  </div>
                  <div className="card">
                      <div className="segment_info">
                          <p>
                              KlipKlapperne
                          </p>
                      </div>
                      <div className="segment_info">
                          <p>
                              5km
                          </p>
                      </div>
                      <div className="segment_info">
                          <p>
                            12
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
                              1324 kr
                          </p>
                          <button>
                              Se mere
                          </button>
                      </div>
                  </div>
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
