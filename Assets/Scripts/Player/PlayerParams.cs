
using UnityEngine;

namespace Trapped_Inside.Param
{
    public class PlayerParam
    {
        public Vector2 inputdir { get; set; }
        public int facedir { get; set; }

        public bool jumpHeld { get; set; }

        public bool jumpDown { get; set; }

        public bool canJump { get; set; } = false;
    }


}
