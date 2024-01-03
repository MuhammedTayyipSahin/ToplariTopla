using System.Linq;
using UnityEngine;


    public class ForceBallsToPoolCommand
    {
        private PlayerManager _manager;
        private PlayerForceData _forceData;

        private readonly string _collectable = "Collectable";

        public ForceBallsToPoolCommand(PlayerManager manager, PlayerForceData forceData)
        {
            _manager = manager;
            _forceData = forceData;
        }

        void Execute()
        {
            var transform1 = _manager.transform;
            var position1 = transform1.position;
            var forcePos = new Vector3(position1.x, position1.y - 1f, position1.z + .9f);

            var collider = Physics.OverlapSphere(forcePos, 1.7f);

            var collectableColliderList = collider.Where(col => col.CompareTag(_collectable)).ToList();

            foreach (var col in collectableColliderList)
            {
                if (col.GetComponent<Rigidbody>() == null) continue;
                var rb = col.GetComponent<Rigidbody>();
                rb.AddForce(new Vector3(0, _forceData.ForceParameters.y, _forceData.ForceParameters.z),
                    ForceMode.Impulse);
            }

            collectableColliderList.Clear();
        }
    }
