import { db } from './database';

// save poll answer in firebase db
export function saveAnswer(pollId, answer) {
  return db.collection(`polls/${pollId}/answers`).add(answer);
}
