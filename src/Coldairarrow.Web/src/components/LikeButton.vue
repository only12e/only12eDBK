<template>
  <div class="like-button-wrapper" :class="[`size-${size}`, { 'is-liked': isLiked }]">
    <div
      class="like-button"
      :class="{ 'liked': isLiked, 'loading': loading, 'pulsing': isAnimating }"
      @click="handleToggleLike"
    >
      <div class="icon-container">
        <span v-if="isLiked" class="heart-icon filled">♥</span>
        <span v-else class="heart-icon outline">♡</span>
        <div v-if="showParticles" class="particles">
          <span v-for="i in 8" :key="i" class="particle" :style="{ '--i': i }"></span>
        </div>
        <div v-if="loading" class="loading-spinner"></div>
      </div>
      <span v-if="showCount" class="like-count">{{ displayCount || 0 }}</span>
    </div>
  </div>
</template>

<script>
import blogLikeApi from '@/api/blog_like'

export default {
  name: 'LikeButton',
  props: {
    // 目标类型: article/project/comment
    targetType: {
      type: String,
      required: true,
      validator: value => ['article', 'project', 'comment'].includes(value)
    },
    // 目标ID
    targetId: {
      type: Number,
      required: true
    },
    // 初始点赞状态
    initialLiked: {
      type: Boolean,
      default: false
    },
    // 初始点赞数量
    initialCount: {
      type: Number,
      default: 0
    },
    // 按钮大小
    size: {
      type: String,
      default: 'default'
    },
    // 是否显示点赞数量
    showCount: {
      type: Boolean,
      default: true
    }
  },
  data() {
    return {
      loading: false,
      isLiked: this.initialLiked,
      likeCount: this.initialCount,
      isAnimating: false,
      showParticles: false
    }
  },
  watch: {
    initialLiked(newVal) {
      this.isLiked = newVal
    },
    initialCount(newVal) {
      this.likeCount = newVal
    }
  },
  computed: {
    displayCount() {
      if (!this.showCount) return ''
      return this.likeCount > 0 ? this.likeCount : ''
    }
  },
  methods: {
    async handleToggleLike() {
      if (this.loading) return

      // 既然用户能进入博客系统，说明已经登录，直接执行点赞操作
      this.loading = true
      try {
        const res = await blogLikeApi.toggleLike(this.targetType, this.targetId)

        if (res.Success) {
          const result = res.Data
          const wasLiked = this.isLiked
          this.isLiked = result.IsLiked
          this.likeCount = result.TotalLikes

          // 如果是新点赞，显示动画效果
          if (!wasLiked && this.isLiked) {
            this.triggerLikeAnimation()
          }

          this.$message.success(result.Message)

          // 触发事件给父组件
          this.$emit('liked', {
            isLiked: this.isLiked,
            likeCount: this.likeCount,
            targetType: this.targetType,
            targetId: this.targetId
          })
        } else {
          this.$message.error(res.Message || '操作失败')
        }
      } catch (error) {
        console.error('点赞操作失败:', error)
        this.$message.error('操作失败，请稍后重试')
      } finally {
        this.loading = false
      }
    },

    // 触发点赞动画
    triggerLikeAnimation() {
      this.isAnimating = true
      this.showParticles = true

      setTimeout(() => {
        this.isAnimating = false
      }, 600)

      setTimeout(() => {
        this.showParticles = false
      }, 1000)
    },

    // 刷新点赞状态
    async refreshLikeStatus() {
      try {
        // 同时获取点赞状态和数量
        const [likedRes, countRes] = await Promise.all([
          blogLikeApi.isLiked(this.targetType, this.targetId),
          blogLikeApi.getLikeCount(this.targetType, this.targetId)
        ])

        this.isLiked = likedRes.Data || false
        this.likeCount = countRes.Data || 0
      } catch (error) {
        console.error('获取点赞状态失败:', error)
        // 发生错误时设置默认值
        this.likeCount = this.initialCount || 0
        this.isLiked = this.initialLiked
      }
    }
  },
  async mounted() {
    // 总是刷新点赞状态
    await this.refreshLikeStatus()
  }
}
</script>

<style lang="less" scoped>
.like-button-wrapper {
  display: inline-block;
  position: relative;

  .like-button {
    display: inline-flex;
    align-items: center;
    gap: 4px;
    padding: 4px 8px;
    border-radius: 6px;
    background: transparent;
    border: none;
    cursor: pointer;
    transition: all 0.2s ease;
    user-select: none;
    position: relative;
    overflow: hidden;
    height: auto;
    font-size: 13px;
    color: #999;
    line-height: 1.5714;
    min-height: 22px;
    box-sizing: border-box;

    &:hover {
      background: rgba(0, 0, 0, 0.04);
      color: #ff4d4f;

      .heart-icon {
        color: #ff4d4f;
        transform: scale(1.1);
      }

      .like-count {
        color: #ff4d4f;
      }
    }

    &:active {
      background: rgba(0, 0, 0, 0.08);
      transform: scale(0.98);
    }

    &.loading {
      pointer-events: none;
      opacity: 0.6;
    }

    &.liked {
      color: #ff4d4f;

      .heart-icon {
        color: #ff4d4f;
      }

      .like-count {
        color: #ff4d4f;
        font-weight: 500;
      }

      &:hover {
        background: rgba(255, 77, 79, 0.06);
        color: #ff7875;

        .heart-icon {
          color: #ff7875;
        }

        .like-count {
          color: #ff7875;
        }
      }
    }

    &.pulsing {
      animation: likeButtonPulse 0.4s ease-in-out;
    }

    .icon-container {
      position: relative;
      display: flex;
      align-items: center;
      justify-content: center;
      width: 16px;
      height: 16px;
      flex-shrink: 0;
      margin-right: 0;
    }

    .heart-icon {
      font-size: 16px;
      transition: all 0.2s ease;
      position: relative;
      z-index: 2;
      display: inline-block;
      line-height: 1;
      user-select: none;

      &.filled {
        animation: heartBeat 0.5s ease-in-out;
      }

      &.outline {
        font-weight: normal;
      }
    }

    .like-count {
      font-size: 13px;
      transition: all 0.2s ease;
      line-height: 1.5714;
      min-width: 12px;
      text-align: left;
    }

    .loading-spinner {
      position: absolute;
      top: 50%;
      left: 50%;
      transform: translate(-50%, -50%);
      width: 10px;
      height: 10px;
      border: 1px solid #ff4d4f;
      border-top: 1px solid transparent;
      border-radius: 50%;
      animation: spin 1s linear infinite;
      z-index: 3;
    }

    .particles {
      position: absolute;
      top: 50%;
      left: 50%;
      transform: translate(-50%, -50%);
      pointer-events: none;
      z-index: 1;

      .particle {
        position: absolute;
        width: 2px;
        height: 2px;
        background: #ff4d4f;
        border-radius: 50%;
        animation: particleExplode 0.8s ease-out forwards;
        animation-delay: calc(var(--i) * 0.04s);

        &:nth-child(1) { --angle: 0deg; }
        &:nth-child(2) { --angle: 45deg; }
        &:nth-child(3) { --angle: 90deg; }
        &:nth-child(4) { --angle: 135deg; }
        &:nth-child(5) { --angle: 180deg; }
        &:nth-child(6) { --angle: 225deg; }
        &:nth-child(7) { --angle: 270deg; }
        &:nth-child(8) { --angle: 315deg; }
      }
    }
  }

  // 所有尺寸统一使用与回复按钮匹配的样式
  &.size-small .like-button,
  &.size-default .like-button,
  &.size-large .like-button {
    padding: 4px 8px;
    gap: 4px;
    font-size: 13px;
    line-height: 1.5714;
    min-height: 22px;

    .icon-container {
      width: 16px;
      height: 16px;
    }

    .heart-icon {
      font-size: 16px;
    }

    .like-count {
      font-size: 13px;
      line-height: 1.5714;
    }
  }
}

// 心跳动画 - 更快更轻
@keyframes heartBeat {
  0%, 50%, 100% {
    transform: scale(1);
  }
  25% {
    transform: scale(1.2);
  }
}

// 按钮脉冲动画 - 更轻微
@keyframes likeButtonPulse {
  0% {
    transform: scale(1);
  }
  50% {
    transform: scale(1.03);
  }
  100% {
    transform: scale(1);
  }
}

// 加载动画
@keyframes spin {
  0% { transform: translate(-50%, -50%) rotate(0deg); }
  100% { transform: translate(-50%, -50%) rotate(360deg); }
}

// 粒子爆炸动画 - 更小范围
@keyframes particleExplode {
  0% {
    opacity: 1;
    transform: rotate(var(--angle)) translateX(0) scale(0);
  }
  50% {
    opacity: 1;
    transform: rotate(var(--angle)) translateX(12px) scale(1);
  }
  100% {
    opacity: 0;
    transform: rotate(var(--angle)) translateX(16px) scale(0);
  }
}

// 响应式设计
@media (max-width: 768px) {
  .like-button-wrapper {
    .like-button {
      &:hover {
        background: transparent;
        color: #999;

        .heart-icon {
          color: inherit;
          transform: none;
        }

        .like-count {
          color: inherit;
        }
      }

      &.liked:hover {
        color: #ff4d4f;

        .heart-icon {
          color: #ff4d4f;
        }

        .like-count {
          color: #ff4d4f;
        }
      }

      &:active {
        background: rgba(0, 0, 0, 0.04);
        transform: scale(0.95);
      }
    }
  }
}
</style>