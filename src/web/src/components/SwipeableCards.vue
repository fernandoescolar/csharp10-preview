<template>
  <section class="container">
    <div class="fixed header">
      <i class="material-icons">refresh</i>
      <span>Hot Crazy C# 10</span>
      <i class="material-icons">tune</i>
    </div>
    <div
      class="fixed fixed--center"
      style="z-index: 3"
      :class="{ 'transition': isVisible }">
      <Vue2InteractDraggable
        v-if="isVisible"
        :interact-out-of-sight-x-coordinate="500"
        :interact-max-rotation="15"
        :interact-x-threshold="200"
        :interact-y-threshold="200"
        :interact-event-bus-events="interactEventBus"
        interact-block-drag-down
        @draggedRight="emitAndNext('match')"
        @draggedLeft="emitAndNext('reject')"
        class="rounded-borders card card--one">
        <div v-if="!finished" style="height: 100%">
          <img
            :src="require(`../assets/images/${current.src}`)"
            class="rounded-borders"/>
          <div class="text">
            <h2>{{current.name}}</h2>
          </div>
        </div>
         <div v-if="finished" style="height: 100%">
          <img
            :src="require(`../assets/images/thanks.jpg`)"
            class="rounded-borders"/>
          <div class="text">
            <h2>Thank you!</h2>
          </div>
        </div>
      </Vue2InteractDraggable>
    </div>
    <div
      v-if="next"
      class="rounded-borders card card--two fixed fixed--center"
      style="z-index: 2">
      <div style="height: 100%">
        <img
          :src="require(`../assets/images/${next.src}`)"
          class="rounded-borders"/>
        <div class="text">
            <h2>{{next.name}}</h2>
          </div>
      </div>
    </div>
    <div
      v-if="index + 2 < cards.length"
      class="rounded-borders card card--three fixed fixed--center"
      style="z-index: 1">
      <div style="height: 100%">
      </div>
    </div>
    <div v-if="!finished" class="footer fixed">
      <div class="btn btn--decline" @click="reject">
          Crazy
      </div>
      <div class="btn btn--like" @click="match">
          Hot
      </div>
    </div>
  </section>
</template>
<script>
import { Vue2InteractDraggable, InteractEventBus } from 'vue2-interact'
import { db } from '../database';

const EVENTS = {
  MATCH: 'match',
  REJECT: 'reject'
}

export default {
  name: 'SwipeableCards',
  components: { Vue2InteractDraggable },
  data() {
    return {
      isVisible: true,
      index: 0,
      interactEventBus: {
        draggedRight: EVENTS.MATCH,
        draggedLeft: EVENTS.REJECT
      },
      cards: [
        { src: '00.jpg', name: 'Test' },
        { src: '00.jpg', name: 'Another Test' },
        { src: '01.jpg', name: 'Global Using' },
        { src: '02.jpg', name: 'Constant interpolated strings' },
        { src: '03.jpg', name: 'Record structs' },
        { src: '04.jpg', name: 'Static abstract members in interfaces' },
        { src: '05.jpg', name: 'File scoped namespaces' },
        { src: '06.jpg', name: 'Lambda improvements' },
        { src: '07.jpg', name: 'Allow Generic Attributes' },
        { src: '08.jpg', name: 'Deconstruction improvements' },
        { src: '09.jpg', name: 'List patterns' },
        { src: '10.jpg', name: 'Property-Scoped Fields' },
        { src: '11.jpg', name: 'Required Properties' }
      ]
    }
  },
  computed: {
    current() {
      return this.cards[this.index]
    },
    finished() {
      return this.index > this.cards.length - 1
    },
    next() {
      return this.cards[this.index + 1]
    }
  },
  methods: {
    match() {
      InteractEventBus.$emit(EVENTS.MATCH)
    },
    reject() {
      InteractEventBus.$emit(EVENTS.REJECT)
    },
    emitAndNext(event) {
      if (event === EVENTS.MATCH)
      {
        const data = { question: this.index, like: true }
        db.collection("poll").add(data)
      }

      if (event === EVENTS.REJECT)
      {
        const data = { question: this.index, like: false }
        db.collection("poll").add(data)
      }

      this.$emit(event, this.index)
      setTimeout(() => this.isVisible = false, 200)
      setTimeout(() => {
        this.index++
        localStorage.setItem('index', this.index)
        this.isVisible = true
      }, 200)
    }
  },
  mounted() {
    const i = localStorage.getItem('index')
    if (i) { this.index = i; }
  }
}
</script>

<style lang="scss" scoped>
.container {
  background: #eceff1;
  width: 100%;
  height: 100vh;
}

.header {
  width: 100%;
  height: 60vh;
  z-index: 0;
  top: 0;
  left: 0;
  color: white;
  text-align: center;
  font-style: italic;
  font-family: 'Engagement', cursive;
  background: #bda5b6;
  background: -webkit-linear-gradient(to top, #b91d73, #f953c6);
  background: linear-gradient(to top, #b91d73, #f953c6);
  clip-path: polygon(0 1%, 100% 0%, 100% 76%, 0 89%);
  display: flex;
  justify-content: space-between;
  span {
    display: block;
    font-size: 4rem;
    padding-top: 2rem;
    text-shadow: 1px 1px 1px red;
  }
  i {
    padding: 24px;
  }
}

.footer {
  width: 77vw;
  bottom: 0;
  left: 50%;
  transform: translateX(-50%);
  display: flex;
  padding-bottom: 30px;
  justify-content: space-around;
  align-items: center;
}

.btn {
  position: relative;
  width: 100px;
  height: 50px;
  text-align: center;
  vertical-align: middle;
  padding: 0.3em 1rem 0.2em 1rem;
  border-radius: 5%;
  background-color: white;
  box-shadow: 0 6px 6px -3px rgba(0,0,0,0.02), 0 10px 14px 1px rgba(0,0,0,0.02), 0 4px 18px 3px rgba(0,0,0,0.02);
  cursor: pointer;
  transition: all .3s ease;
  user-select: none;
  -webkit-tap-highlight-color:transparent;
  &:active {
    transform: translateY(4px);
  }
  i {
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    &::before {
      content: '';
    }
  }
  &--like {
    background-color: #f953c6;
    color: white;
    box-shadow: 0 10px 13px -6px rgba(0,0,0,.2), 0 20px 31px 3px rgba(0,0,0,.14), 0 8px 38px 7px rgba(0,0,0,.12);
    font-size: 40px;
  }
  &--decline {
    color: bda5b6;
        box-shadow: 0 10px 13px -6px rgba(0,0,0,.2), 0 20px 31px 3px rgba(0,0,0,.14), 0 8px 38px 7px rgba(0,0,0,.12);
    font-size: 40px;
  }
}

.flex {
  display: flex;
  &--center {
    align-items: center;
    justify-content: center;
  }
}

.fixed {
  position: fixed;
  &--center {
    left: 50%;
    top: 50%;
    transform: translate(-50%, -50%);
  }
}
.rounded-borders {
  border-radius: 12px;
}
.card {
  width: 80vw;
  height: 60vh;
  color: white;
  img {
    object-fit: cover;
    display: block;
    width: 100%;
    height: 100%;
  }
  &--one {
    box-shadow: 0 1px 3px rgba(0,0,0,.2), 0 1px 1px rgba(0,0,0,.14), 0 2px 1px -1px rgba(0,0,0,.12);
  }
  &--two {
    transform: translate(-48%, -48%);
    box-shadow: 0 6px 6px -3px rgba(0,0,0,.2), 0 10px 14px 1px rgba(0,0,0,.14), 0 4px 18px 3px rgba(0,0,0,.12);
  }
  &--three {
    background: rgba(black, .8);
    transform: translate(-46%, -46%);
    box-shadow: 0 10px 13px -6px rgba(0,0,0,.2), 0 20px 31px 3px rgba(0,0,0,.14), 0 8px 38px 7px rgba(0,0,0,.12);
  }
  .text {
    position: absolute;
    bottom: 0;
    width: 100%;
    background:black;
    background:rgba(0,0,0,0.5);
    border-bottom-right-radius: 12px;
    border-bottom-left-radius: 12px;
    text-indent: 20px;
    span {
      font-weight: normal;
    }
  }
}

.transition {
  animation: appear 200ms ease-in;
}

@keyframes appear {
  from {
    transform: translate(-48%, -48%);
  }
  to {
    transform: translate(-50%, -50%);
  }
}
</style>
