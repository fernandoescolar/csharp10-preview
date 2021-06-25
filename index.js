import 'reveal.js/dist/reveal.css';
import './classic-dark.css';
import 'reveal.js/dist/theme/moon.css'; // beige, black, blood, league, moon, night, serif, simple, ...
//import 'reveal.js/plugin/highlight/monokai.css'; // zenburn

import Reveal from 'reveal.js';
import Markdown from 'reveal.js/plugin/markdown/markdown.esm.js';
import Highlight from 'reveal.js/plugin/highlight/highlight.esm.js';
import Notes from 'reveal.js/plugin/notes/notes.esm.js';
import Zoom from 'reveal.js/plugin/zoom/zoom.esm.js';

const deck = new Reveal({
  plugins: [ Markdown, Highlight, Notes, Zoom ]
});

deck.initialize({ hash: true, progress: true });
